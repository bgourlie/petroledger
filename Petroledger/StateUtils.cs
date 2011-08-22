using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Petroledger
{
    public static class StateUtils
    {
        static List<Action> workItems;

        public static void PreserveState(IDictionary<string, object> state, TextBox textBox)
        {
            state[textBox.Name + "_Text"] = textBox.Text;
            state[textBox.Name + "_SelectionStart"] = textBox.SelectionStart;
            state[textBox.Name + "_SelectionLength"] = textBox.SelectionLength;
        }

        
        public static void RestoreState(IDictionary<string, object> state, TextBox textBox, string defaultValue)
        {
            textBox.Text = TryGetValue(state, textBox.Name + "_Text", defaultValue);
            textBox.SelectionStart = TryGetValue(state, textBox.Name + "_SelectionStart", textBox.Text.Length);
            textBox.SelectionLength = TryGetValue(state, textBox.Name + "_SelectionLength", 0);
        }
        
        public static void PreserveState(IDictionary<string, object> state, DatePicker datePicker)
        {
            state[datePicker.Name + "_Value"] = datePicker.Value;
        }

        public static void RestoreState(IDictionary<string, object> state, DatePicker datePicker, DateTime? defaultValue)
        {
            datePicker.Value = TryGetValue(state, datePicker.Name + "_Value", defaultValue);
        }

        // Preserve and restore methods for RadioButton
        public static void PreserveState(IDictionary<string, object> state, RadioButton radioButton)
        {
            state[radioButton.Name + "_IsChecked"] = radioButton.IsChecked;
        }

        public static void RestoreState(IDictionary<string, object> state, RadioButton radioButton, bool defaultValue)
        {
            radioButton.IsChecked = TryGetValue<bool>(state, radioButton.Name + "_IsChecked", defaultValue);
        }

        // Preserve and restore methods for Checkbox
        public static void PreserveState(IDictionary<string, object> state, CheckBox checkBox)
        {
            state[checkBox.Name + "_IsChecked"] = checkBox.IsChecked;
        }
        public static void RestoreState(IDictionary<string, object> state, CheckBox checkBox, bool defaultValue)
        {
            checkBox.IsChecked = TryGetValue<bool>(state, checkBox.Name + "_IsChecked", defaultValue);
        }

        public static void PreserveFocusState(IDictionary<string, object> state, FrameworkElement parent)
        {
            // Determine which control currently has focus.
            var focusedControl = FocusManager.GetFocusedElement() as Control;

            // If no control has focus, store null in the State dictionary.
            if (focusedControl == null)
            {
                state["FocusedControlName"] = null;
                return;
            }
            
            // Find the control within the parent
            var foundFE = parent.FindName(focusedControl.Name) as Control;

            // If the control isn't found within the parent, store null in the State dictionary.
            if (foundFE == null)
            {
                state["FocusedElementName"] = null;
            }
            else
            {
                // otherwise store the name of the control with focus.
                state["FocusedElementName"] = focusedControl.Name;
            }
            
        }

        public static void RestoreFocusState(IDictionary<string, object> state, FrameworkElement parent)
        {
            // Get the name of the control that should have focus.
            string focusedName = TryGetValue<string>(state, "FocusedElementName", null);

            // Check to see if the name is null or empty
            if (!String.IsNullOrEmpty(focusedName))
            {

                // Find the control name in the parent.
                var focusedControl = parent.FindName(focusedName) as Control;
                if (focusedControl != null)
                {
                    // If the control is found, schedule a call to its Focus method for the next render.
                    ScheduleOnNextRender(() => focusedControl.Focus());
                }
            }
        }

        public static void ScheduleOnNextRender(Action action)
        {
            // If the list of work items is null, create a new one and register DoWorkOnRender as a 
            // handler for the CompositionTarget.Rendering event.
            if (workItems == null)
            {
                workItems = new List<Action>();
                CompositionTarget.Rendering += DoWorkOnRender;
            }

            // Add the supplied action to the list.
            workItems.Add(action);
        }

        static void DoWorkOnRender(object sender, EventArgs args)
        {
            // Remove ourselves from the event and clear the list
            CompositionTarget.Rendering -= DoWorkOnRender;
            List<Action> work = workItems;
            workItems = null;

            // Loop through each action in the list
            foreach (Action action in work)
            {
                action();
            }
        }

        private static T TryGetValue<T>(IDictionary<string, object> state, string name, T defaultValue)
        {
            if (state.ContainsKey(name))
            {
                if (state[name] != null)
                {
                    return (T)state[name];
                }
            }
            return defaultValue;
        }
    }
}
