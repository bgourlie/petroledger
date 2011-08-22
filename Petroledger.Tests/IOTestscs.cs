using System;
using System.IO.IsolatedStorage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroledger.Data.Model;
using Petroledger.Extensions;

namespace Petroledger.Tests
{
    [TestClass]
    public class IoTests
    {


        [TestInitialize]
        public void Setup()
        {
            //delete all files from isolated storage
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                foreach (var fileName in store.GetFileNames())
                {
                    store.DeleteFile(fileName);
                }

                foreach (string directoryName in store.GetDirectoryNames())
                {
                    Util.RecursiveDeleteDirectory(directoryName, store);
                }
            }
        }

        [TestMethod]
        public void TestDeleteEntry()
        {
            var vehicle = new Vehicle {VehicleName = "test vehicle"};
            Vehicle.Save(vehicle);


            var entry = new FillupEntry
                            {
                                EntryDate = DateTime.Now,
                                FillAmount = 13.564,
                                OdometerReading = 100215,
                                PricePerUnit = 2.899
                            };

            vehicle.Entries.Add(entry);
            vehicle.SaveEntry(entry);

            vehicle.DeleteFillupEntry(entry);

            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Assert.IsFalse(store.FileExists(vehicle.Id + "\\" + entry.OdometerReading));
            }
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException), "The entry must exist in the Vehicle's Entry collection.")]
        public void TestDeleteEntryNotAssociatedWithVehicle()
        {
            var vehicle = new Vehicle {VehicleName = "test vehicle"};
            Vehicle.Save(vehicle);


            var entry = new FillupEntry
                            {
                                EntryDate = DateTime.Now,
                                FillAmount = 13.564,
                                OdometerReading = 100215,
                                PricePerUnit = 2.899
                            };

            vehicle.SaveEntry(entry);

            vehicle.DeleteFillupEntry(entry);
        }

        [TestMethod]
        public void SmokeTestUpdateEntry()
        {
            var vehicle = new Vehicle { VehicleName = "test vehicle" };
            Vehicle.Save(vehicle);


            var entry = new FillupEntry
            {
                EntryDate = DateTime.Now,
                FillAmount = 13.564,
                OdometerReading = 100215,
                PricePerUnit = 2.899
            };
            var originalTicks = entry.EntryDate.Ticks;

            vehicle.SaveEntry(entry);

            vehicle.Entries.Add(entry);

            entry.OdometerReading = 100210;

            vehicle.UpdateExistingEntry(entry, originalTicks);

            entry.EntryDate = new DateTime(2010,10,15);

            vehicle.UpdateExistingEntry(entry, originalTicks);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The entry must exist in the Vehicle's Entry collection.")]
        public void TestUpdateEntryThrowsArgumentExceptionEntryMustBelongToVehicle()
        {
            var vehicle = new Vehicle { VehicleName = "test vehicle" };
            Vehicle.Save(vehicle);
            
            var entry = new FillupEntry
            {
                EntryDate = DateTime.Now,
                FillAmount = 13.564,
                OdometerReading = 100215,
                PricePerUnit = 2.899
            };
            var originalTicks = entry.EntryDate.Ticks;

            vehicle.SaveEntry(entry);

            entry.OdometerReading = 100210;

            vehicle.UpdateExistingEntry(entry, originalTicks);
        }
    }
}