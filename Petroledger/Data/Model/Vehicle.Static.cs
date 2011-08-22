using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using Petroledger.Exceptions;

namespace Petroledger.Data.Model
{
    public partial class Vehicle
    {
        private const int VEHICLE_SIZE = 125;
        private const int MAKE_LENGTH = sizeof (char)*20;
        private const int MODEL_LENGTH = sizeof (char)*20;
        private const int LABEL_LENGTH = sizeof (char)*20;

        public static void AssignId(Vehicle vehicle, int Id)
        {
            if(vehicle.Id != -1)
                throw new InvalidOperationException("Cannot call AssignId for a vehicle that has already been assigned an Id.");

            vehicle.Id = Id;
        }

        public static void Save(Vehicle vehicle, int id = -1)
        {
            Debug.Assert(!String.IsNullOrEmpty(vehicle.VehicleName.Trim()), "The vehicle's VehicleName property cannot be empty.");

            if (App.VehicleStore.Values.Any(v => v.Id != vehicle.Id && v.VehicleName.Equals(vehicle.VehicleName)))
                throw new DuplicateVehicleLabelException("A vehicle already exists with this label.");

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //determine a new id for the vehicle if it's new
                if (id == -1)
                {
                    while (store.DirectoryExists((++id).ToString())){}
                    store.CreateDirectory(id.ToString());
                }

                vehicle.Id = id;
                string fileName = id + @"\vehicle";
                Debug.WriteLine("Saving '{0}' to file '{1}'", vehicle.VehicleName, fileName);
                using (var fileWriter = store.CreateFile(fileName))
                {
                    byte[] bytes = ToBytes(vehicle);
                    fileWriter.Write(bytes, 0, bytes.Length);
                }
            }
        }

        public static List<Vehicle> LoadVehiclesFromStorage()
        {
            var retval = new List<Vehicle>();
            //load vehicles from isolated storage
            int id = -1;

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                while (store.DirectoryExists((++id).ToString()))
                {
                    string fileName = id + @"\vehicle";

                    using (var fileStream = store.OpenFile(fileName, FileMode.Open, FileAccess.Read))
                    {
                        var vehicleBytes = new byte[VEHICLE_SIZE];
                        fileStream.Read(vehicleBytes, 0, vehicleBytes.Length);
                        Vehicle vehicle = FromBytes(vehicleBytes);
                        vehicle.Id = id;
                        retval.Add(vehicle);
                    }
                }
            }

            return retval;
        }

        public static byte[] ToBytes(Vehicle vehicle)
        {
            var vehicleBytes =
                new byte[VEHICLE_SIZE];

            int curOffset = 0;

            byte[] makeBytes = vehicle.Make != null
                                   ? Encoding.Unicode.GetBytes(vehicle.Make.Trim())
                                   : new byte[MAKE_LENGTH];

            Array.Copy(makeBytes, 0, vehicleBytes, curOffset,
                       makeBytes.Length < MAKE_LENGTH ? makeBytes.Length : MAKE_LENGTH);
            curOffset += MAKE_LENGTH;

            byte[] modelBytes = vehicle.Model != null
                                    ? Encoding.Unicode.GetBytes(vehicle.Model.Trim())
                                    : new byte[MODEL_LENGTH];

            Array.Copy(modelBytes, 0, vehicleBytes, curOffset,
                       modelBytes.Length < MODEL_LENGTH ? modelBytes.Length : MODEL_LENGTH);

            curOffset += MODEL_LENGTH;

            byte[] labelBytes = Encoding.Unicode.GetBytes(vehicle.VehicleName.Trim());
            Array.Copy(labelBytes, 0, vehicleBytes, curOffset,
                       labelBytes.Length < LABEL_LENGTH ? labelBytes.Length : LABEL_LENGTH);

            curOffset += LABEL_LENGTH;

            Array.Copy(BitConverter.GetBytes(vehicle.Year), 0, vehicleBytes, curOffset, sizeof (int));
            curOffset += sizeof (int);

            Array.Copy(new[] {(byte) vehicle.OdometerUnit}, 0, vehicleBytes, curOffset, sizeof (byte));

            return vehicleBytes;
        }

        public static Vehicle FromBytes(byte[] bytes)
        {
            var vehicle = new Vehicle();
            int curOffset = 0;
            string makeString = Encoding.Unicode.GetString(bytes, curOffset, MAKE_LENGTH);
            curOffset += MAKE_LENGTH;
            vehicle.Make = makeString.Replace("\0", string.Empty);

            string modelString = Encoding.Unicode.GetString(bytes, curOffset, MODEL_LENGTH);
            vehicle.Model = modelString.Replace("\0", string.Empty);
            curOffset += MODEL_LENGTH;

            string labelString = Encoding.Unicode.GetString(bytes, curOffset, LABEL_LENGTH);
            vehicle.VehicleName = labelString.Replace("\0", string.Empty);
            curOffset += LABEL_LENGTH;

            var yearBytes = new byte[sizeof (int)];
            Array.Copy(bytes, curOffset, yearBytes, 0, yearBytes.Length);
            vehicle.Year = BitConverter.ToInt32(yearBytes, 0);
            curOffset += sizeof (int);

            var odometerBytes = new byte[sizeof (byte)];
            Array.Copy(bytes, curOffset, odometerBytes, 0, odometerBytes.Length);
            vehicle.OdometerUnit = (UnitOfMeasure.Distance) odometerBytes[0];

            return vehicle;
        }
    }
}