using System;
using System.Globalization;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroledger.Data;
using Petroledger.Data.Model;
using Petroledger.Extensions;

namespace Petroledger.Tests
{
    [TestClass]
    public class ModelTests : SilverlightTest
    {
        [TestMethod]
        public void TestVehicleConstruction()
        {
            var vehicle = new Vehicle();
            Assert.IsNotNull(vehicle.Entries);
        }

        [TestMethod]
        public void TestVehicleSerializationAndDeserialization()
        {
            var vehicle = new Vehicle
                              {
                                  Year = 1995,
                                  Make = "Honda",
                                  Model = "Accord",
                                  VehicleName = "'05 Honda Accord",
                                  OdometerUnit = UnitOfMeasure.Distance.Mile
                              };

            byte[] vehicleBytes = Vehicle.ToBytes(vehicle);
            Vehicle vehicle2 = Vehicle.FromBytes(vehicleBytes);

            Assert.AreEqual(vehicle.Year, vehicle2.Year);
            Assert.AreEqual(vehicle.Make, vehicle2.Make);
            Assert.AreEqual(vehicle.Model, vehicle2.Model);
            Assert.AreEqual(vehicle.VehicleName, vehicle2.VehicleName);
            Assert.AreEqual(vehicle.OdometerUnit, vehicle2.OdometerUnit);
        }

        [TestMethod]
        public void TestEntrySerializationAndDeserialization()
        {
            var entry = new FillupEntry
                            {
                                FillAmount = 13.2342352353,
                                PricePerUnit = 3.4543,
                                PumpUnit = UnitOfMeasure.Volume.Gallon,
                                OdometerUnit = UnitOfMeasure.Distance.Mile,
                                EntryDate = DateTime.Now,
                                OdometerReading = 143423.4,
                                WasNotToppedOff = false,
                                PreviousEntryMissed = true
                            };

            byte[] entryBytes = entry.ToBytes();
            FillupEntry entry2 = FillupEntryExtensions.EntryFromBytes(entryBytes);

            Assert.AreEqual(entry.FillAmount, entry2.FillAmount);
            Assert.AreEqual(entry.EntryDate, entry2.EntryDate);
            Assert.AreEqual(entry.OdometerReading, entry2.OdometerReading);
            Assert.AreEqual(entry.OdometerUnit, entry2.OdometerUnit);
            Assert.AreEqual(entry.PricePerUnit, entry2.PricePerUnit);
            Assert.AreEqual(entry.PumpUnit, entry2.PumpUnit);
            Assert.AreEqual(entry.WasNotToppedOff, entry2.WasNotToppedOff);
            Assert.AreEqual(entry.PreviousEntryMissed, entry2.PreviousEntryMissed);
        }

        [TestMethod]
        public void TestFillupEntryClone()
        {
            var fillupEntry = new FillupEntry
                                  {
                                      FillAmount = 3.154,
                                      OdometerReading = 154254.6,
                                      EntryDate = DateTime.Now,
                                      PreviousEntryMissed = true,
                                      PricePerUnit = 3.256,
                                      PumpUnit = UnitOfMeasure.Volume.Gallon,
                                      WasNotToppedOff = true
                                  };

            FillupEntry fillupEntryClone = FillupEntry.Clone(fillupEntry);

            Assert.IsFalse(ReferenceEquals(fillupEntry, fillupEntryClone));
            Assert.AreEqual(fillupEntry.EntryDate, fillupEntryClone.EntryDate);
            Assert.IsFalse(ReferenceEquals(fillupEntry.EntryDate, fillupEntryClone.EntryDate));
            Assert.AreEqual(fillupEntry.FillAmount, fillupEntryClone.FillAmount);
            Assert.IsFalse(ReferenceEquals(fillupEntry.FillAmount, fillupEntryClone.FillAmount));
            Assert.AreEqual(fillupEntry.OdometerReading, fillupEntryClone.OdometerReading);
            Assert.IsFalse(ReferenceEquals(fillupEntry.OdometerReading, fillupEntryClone.OdometerReading));
            Assert.AreEqual(fillupEntry.PreviousEntryMissed, fillupEntryClone.PreviousEntryMissed);
            Assert.IsFalse(ReferenceEquals(fillupEntry.PreviousEntryMissed, fillupEntryClone.PreviousEntryMissed));
            Assert.AreEqual(fillupEntry.PumpUnit, fillupEntryClone.PumpUnit);
            Assert.IsFalse(ReferenceEquals(fillupEntry.PumpUnit, fillupEntryClone.PumpUnit));
        }

        [TestMethod]
        public void TestInsertEntry()
        {
            var vehicle = new Vehicle {OdometerUnit = UnitOfMeasure.DefaultDistanceUnit};
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,1,1),
                                        OdometerReading = 100,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,1,15),
                                        OdometerReading = 200,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,1,30),
                                        OdometerReading = 300,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,2,1),
                                        OdometerReading = 400,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,2,15),
                                        OdometerReading = 500,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });

            var entry = new FillupEntry
                            {
                                EntryDate = new DateTime(2009,12,20),
                                OdometerReading = 50,
                                FillAmount = 10,
                                OdometerUnit = vehicle.OdometerUnit
                            };

            vehicle.InsertEntry(entry);
            Assert.IsTrue(vehicle.Entries.IndexOf(entry) == 0);

            entry = new FillupEntry
                        {
                            EntryDate = new DateTime(2010,2,28),
                            OdometerReading = 600,
                            FillAmount = 10,
                            OdometerUnit = vehicle.OdometerUnit
                        };

            vehicle.InsertEntry(entry);
            Assert.IsTrue(vehicle.Entries.IndexOf(entry) == vehicle.Entries.Count - 1);


            entry = new FillupEntry
                        {
                            EntryDate = new DateTime(2009,12,25),
                            OdometerReading = 75,
                            FillAmount = 10,
                            OdometerUnit = vehicle.OdometerUnit
                        };

            vehicle.InsertEntry(entry);
            Assert.IsTrue(vehicle.Entries.IndexOf(entry) == 1);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException),
            "The fillup entry's odometer reading is greater than or equal to a reading with a later date.")]
        public void TestInsertEntryOdometerHigherThanLaterDate()
        {
            var vehicle = new Vehicle {OdometerUnit = UnitOfMeasure.DefaultDistanceUnit};
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,1,1),
                                        OdometerReading = 100,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,1,15),
                                        OdometerReading = 200,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,1,30),
                                        OdometerReading = 300,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,2,1),
                                        OdometerReading = 400,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,2,15),
                                        OdometerReading = 500,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });

            var entry = new FillupEntry
                            {
                                EntryDate = new DateTime(2010,1,20),
                                OdometerReading = 600,
                                FillAmount = 10,
                                OdometerUnit = vehicle.OdometerUnit
                            };

            vehicle.InsertEntry(entry);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException),
            "The fillup entry's odometer reading is less than or equal to a reading with an earlier date.")]
        public void TestInsertEntryOdometerLessThanEarlierDate()
        {
            var vehicle = new Vehicle {OdometerUnit = UnitOfMeasure.DefaultDistanceUnit};
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,1,1),
                                        OdometerReading = 100,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,1,15),
                                        OdometerReading = 200,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,1,30),
                                        OdometerReading = 300,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,2,1),
                                        OdometerReading = 400,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,2,15),
                                        OdometerReading = 500,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });

            var entry = new FillupEntry
                            {
                                EntryDate = new DateTime(2010,1,20),
                                OdometerReading = 50,
                                FillAmount = 10,
                                OdometerUnit = vehicle.OdometerUnit
                            };

            vehicle.InsertEntry(entry);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException),
            "The fillup entry's odometer reading is less than or equal to a reading with an earlier date.")]
        public void TestInsertEntryOdometerGreaterThanLaterDateAtZeroIndex()
        {
            var vehicle = new Vehicle {OdometerUnit = UnitOfMeasure.DefaultDistanceUnit};
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,1,1),
                                        OdometerReading = 100,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,1,15),
                                        OdometerReading = 200,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,1,30),
                                        OdometerReading = 300,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,2,1),
                                        OdometerReading = 400,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });
            vehicle.Entries.Add(new FillupEntry
                                    {
                                        EntryDate = new DateTime(2010,2,15),
                                        OdometerReading = 500,
                                        FillAmount = 10,
                                        OdometerUnit = vehicle.OdometerUnit
                                    });

            var entry = new FillupEntry
                            {
                                EntryDate = new DateTime(2009,12,25),
                                OdometerReading = 200,
                                FillAmount = 10,
                                OdometerUnit = vehicle.OdometerUnit
                            };

            vehicle.InsertEntry(entry);
        }
    }
}