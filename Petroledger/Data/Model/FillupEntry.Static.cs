using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Petroledger.Data.Model
{

    public partial class FillupEntry
    {
        public const int SIZE_IN_BYTES = 39;

        public static FillupEntry Clone(FillupEntry entry)
        {
            return (FillupEntry)entry.MemberwiseClone();
        }

        public bool Equals(FillupEntry other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.EntryDate.Equals(EntryDate);
        }

        //equals assumes that the entries belong to the same vehicles
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(FillupEntry)) return false;
            return Equals((FillupEntry)obj);
        }

        public override int GetHashCode()
        {
            return EntryDate.GetHashCode();
        }
    }
}
