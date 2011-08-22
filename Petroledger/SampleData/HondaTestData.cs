using System;
using Petroledger.Data;
using Petroledger.Data.Model;

namespace Petroledger.SampleData
{
    //to replace DateTime.Parse("mm/dd/yyyy")
    //with format-agnostic constructor use the following regex find and replace:
    //Find:  DateTime.Parse\("{:d+}/{:d+}/{:d+}"\)
    //Replace: new DateTime(\3,\1,\2)


    public static class HondaTestData
    {
        public static Vehicle SampleVehicle()
        {
            var honda = new Vehicle
                            {
                                Make = "Sample",
                                Model = "Vehicle",
                                Year = 2005,
                                OdometerUnit = UnitOfMeasure.DefaultDistanceUnit,
                                VehicleName = "sample car"
                            };

            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2005,9,28),
            //                          FillAmount = 14.136,
            //                          PricePerUnit = 2.759,
            //                          OdometerReading = 1008,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2005,9,30),
            //                          FillAmount = 17.513,
            //                          PricePerUnit = 2.999,
            //                          OdometerReading = 1385,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2005,9,30).AddMilliseconds(5000),
            //                          FillAmount = 7,
            //                          PricePerUnit = 2.999,
            //                          OdometerReading = 1548,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2005,10,16),
            //                          FillAmount = 11.62,
            //                          PricePerUnit = 2.659,
            //                          OdometerReading = 1780,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2005,11,5),
            //                          FillAmount = 15.002,
            //                          PricePerUnit = 2.399,
            //                          OdometerReading = 2127,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2005,11,7),
            //                          FillAmount = 16.334,
            //                          
            //                          PricePerUnit = 2.199,
            //                          OdometerReading = 2519,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2005,11,9),
            //                          FillAmount = 11.589,
            //                          PricePerUnit = 2.389,
            //                          
            //                          OdometerReading = 2755,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2005,11,17),
            //                          FillAmount = 13.684,
            //                          PricePerUnit = 2.229,
            //                          
            //                          OdometerReading = 3009,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2005,11,29),
            //                          FillAmount = 17.254,
            //                          PricePerUnit = 2.199,
            //                          OdometerReading = 3336,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2005,12,31),
            //                          FillAmount = 18.18,
            //                          PricePerUnit = 2.299,
            //                          OdometerReading = 3644,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,01,06),
            //                          FillAmount = 14.84,
            //                          PricePerUnit = 2.399,
            //                          OdometerReading = 3977,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,01,17),
            //                          FillAmount = 10.41,
            //                          PricePerUnit = 2.359,
            //                          OdometerReading = 4175,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,01,24),
            //                          FillAmount = 16.575,
            //                          PricePerUnit = 2.359,
            //                          
            //                          OdometerReading = 4534,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,02,01),
            //                          FillAmount = 19.36,
            //                          PricePerUnit = 2.349,
            //                          
            //                          OdometerReading = 4901,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,02,18),
            //                          FillAmount = 13.998,
            //                          PricePerUnit = 2.299,
            //                          
            //                          OdometerReading = 5151,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,02,19),
            //                          FillAmount = 17.649,
            //                          PricePerUnit = 2.299,
            //                          
            //                          OdometerReading = 5525,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,04,04),
            //                          FillAmount = 16.497,
            //                          PricePerUnit = 2.659,
            //                          
            //                          OdometerReading = 5803,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,04,16),
            //                          FillAmount = 19.235,
            //                          PricePerUnit = 2.799,
            //                          
            //                          OdometerReading = 6170,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,04,22),
            //                          FillAmount = 16.47,
            //                          PricePerUnit = 2.899,
            //                          
            //                          OdometerReading = 6553,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,05,09),
            //                          FillAmount = 17.442,
            //                          PricePerUnit = 2.849,
            //                          OdometerReading = 6952,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,05,23),
            //                          FillAmount = 15.108,
            //                          PricePerUnit = 2.859,
            //                          OdometerReading = 7215,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,06,07),
            //                          FillAmount = 18,
            //                          PricePerUnit = 2.939,
            //                          
            //                          OdometerReading = 7559,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,06,11),
            //                          FillAmount = 18.190,
            //                          
            //                          PricePerUnit = 2.959,
            //                          OdometerReading = 7980,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,07,09),
            //                          FillAmount = 14.838,
            //                          
            //                          PricePerUnit = 3.079,
            //                          OdometerReading = 8317,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,07,19),
            //                          FillAmount = 15.151,
            //                          PricePerUnit = 3.079,
            //                          
            //                          OdometerReading = 8654,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,08,06),
            //                          FillAmount = 17.766,
            //                          PricePerUnit = 3.299,
            //                          
            //                          OdometerReading = 9009,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,08,06).AddMilliseconds(10),
            //                          FillAmount = 11.202,
            //                          PricePerUnit = 3.299,
            //                          
            //                          OdometerReading = 9209,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,08,09),
            //                          FillAmount = 13.486,
            //                          PricePerUnit = 3.149,
            //                          
            //                          OdometerReading = 9521,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,08,12),
            //                          FillAmount = 18.326,
            //                          PricePerUnit = 3.139,
            //                          
            //                          OdometerReading = 9855,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,08,16),
            //                          FillAmount = 8.584,
            //                          PricePerUnit = 3.129,
            //                          
            //                          OdometerReading = 10000,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,09,08),
            //                          FillAmount = 17.724,
            //                          PricePerUnit = 2.699,
            //                          
            //                          OdometerReading = 10391,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,09,19),
            //                          FillAmount = 18.003,
            //                          PricePerUnit = 2.479,
            //                          
            //                          OdometerReading = 10714,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,09,29),
            //                          FillAmount = 13.803,
            //                          PricePerUnit = 2.359,
            //                          
            //                          OdometerReading = 11013,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,10,01),
            //                          FillAmount = 15.663,
            //                          PricePerUnit = 2.329,
            //                          
            //                          OdometerReading = 11385,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,10,08),
            //                          FillAmount = 16.4,
            //                          PricePerUnit = 2.256,
            //                          OdometerReading = 11749,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,10,20),
            //                          FillAmount = 16.674,
            //                          PricePerUnit = 2.259,
            //                          OdometerReading = 12070,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,11,4),
            //                          FillAmount = 14.084,
            //                          PricePerUnit = 2.259,
            //                          
            //                          OdometerReading = 12325,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,11,17),
            //                          FillAmount = 15.536,
            //                          
            //                          PricePerUnit = 2.259,
            //                          OdometerReading = 12645,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,11,29),
            //                          FillAmount = 18.16,
            //                          PricePerUnit = 2.259,
            //                          
            //                          OdometerReading = 12986,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,11,30),
            //                          FillAmount = 10.174,
            //                          PricePerUnit = 2.259,
            //                          
            //                          OdometerReading = 13207,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2006,12,24),
            //                          FillAmount = 16.710,
            //                          PricePerUnit = 2.359,
            //                          
            //                          OdometerReading = 13507,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,01,01),
            //                          FillAmount = 11.230,
            //                          PricePerUnit = 2.279,
            //                          
            //                          OdometerReading = 13708,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,01,08),
            //                          FillAmount = 16.333,
            //                          
            //                          PricePerUnit = 2.299,
            //                          OdometerReading = 14089,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,01,26),
            //                          FillAmount = 14.37,
            //                          
            //                          PricePerUnit = 2.159,
            //                          OdometerReading = 14330,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,02,11),
            //                          FillAmount = 9.228,
            //                          PricePerUnit = 2.269,
            //                          
            //                          OdometerReading = 14486,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,02,12),
            //                          FillAmount = 16.341,
            //                          
            //                          PricePerUnit = 2.249,
            //                          OdometerReading = 14868,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,03,16),
            //                          FillAmount = 17.696,
            //                          
            //                          PricePerUnit = 2.499,
            //                          OdometerReading = 15159,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,04,07),
            //                          FillAmount = 17.863,
            //                          PricePerUnit = 2.799,
            //                          
            //                          OdometerReading = 15499,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,04,08),
            //                          FillAmount = 10.141,
            //                          PricePerUnit = 2.759,
            //                          OdometerReading = 15738,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,04,19),
            //                          FillAmount = 17.86,
            //                          PricePerUnit = 2.799,
            //                          OdometerReading = 16124,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,05,11),
            //                          FillAmount = 16.411,
            //                          PricePerUnit = 3.199,
            //                          OdometerReading = 16438,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,05,18),
            //                          FillAmount = 11.683,
            //                          PricePerUnit = 3.299,
            //                          OdometerReading = 16657,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,05,19),
            //                          FillAmount = 8.541,
            //                          PricePerUnit = 3.399,
            //                          OdometerReading = 16830,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,06,14),
            //                          FillAmount = 18.643,
            //                          PricePerUnit = 3.129,
            //                          
            //                          OdometerReading = 17270,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,07,01),
            //                          FillAmount = 17.615,
            //                          PricePerUnit = 3.039,
            //                          
            //                          OdometerReading = 17638,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,07,14),
            //                          FillAmount = 17.539,
            //                          
            //                          PricePerUnit = 3.199,
            //                          OdometerReading = 17992,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,07,23),
            //                          FillAmount = 16.787,
            //                          PricePerUnit = 3.189,
            //                          
            //                          OdometerReading = 18290,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,07,24),
            //                          FillAmount = 15.301,
            //                          PricePerUnit = 3.179,
            //                          
            //                          OdometerReading = 18570,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,07,29),
            //                          FillAmount = 18.109,
            //                          
            //                          PricePerUnit = 2.969,
            //                          OdometerReading = 18982,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,07,31),
            //                          FillAmount = 15.178,
            //                          PricePerUnit = 2.999,
            //                          
            //                          OdometerReading = 19236,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,08,04),
            //                          FillAmount = 17,
            //                          PricePerUnit = 2.999,
            //                          
            //                          OdometerReading = 19552,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,08,11),
            //                          FillAmount = 18.285,
            //                          
            //                          PricePerUnit = 2.859,
            //                          OdometerReading = 19861,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,08,24),
            //                          FillAmount = 15.873,
            //                          
            //                          PricePerUnit = 3.099,
            //                          OdometerReading = 20218,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,09,01),
            //                          FillAmount = 14.878,
            //                          PricePerUnit = 3.059,
            //                          
            //                          OdometerReading = 20481,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,09,13),
            //                          FillAmount = 17.605,
            //                          PricePerUnit = 2.939,
            //                          OdometerReading = 20847,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,09,23),
            //                          FillAmount = 18.002,
            //                          PricePerUnit = 2.919,
            //                          
            //                          OdometerReading = 21179,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,10,13),
            //                          FillAmount = 18.703,
            //                          PricePerUnit = 2.969,
            //                          
            //                          OdometerReading = 21554,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,11,09),
            //                          FillAmount = 17.896,
            //                          PricePerUnit = 3.159,
            //                          OdometerReading = 21866,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,11,16),
            //                          FillAmount = 14,
            //                          PricePerUnit = 3.219,
            //                          OdometerReading = 22172,
            //                          
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,12,09),
            //                          FillAmount = 17.978,
            //                          PricePerUnit = 2.999,
            //                          
            //                          OdometerReading = 22499,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,12,15),
            //                          FillAmount = 14.851,
            //                          
            //                          PricePerUnit = 2.989,
            //                          OdometerReading = 22785,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,12,21),
            //                          FillAmount = 14.951,
            //                          
            //                          PricePerUnit = 2.979,
            //                          OdometerReading = 23085,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2007,12,29),
            //                          FillAmount = 9.775,
            //                          
            //                          PricePerUnit = 3.099,
            //                          OdometerReading = 23242,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2008,01,01),
            //                          FillAmount = 17.976,
            //                          
            //                          PricePerUnit = 3.099,
            //                          OdometerReading = 23649,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            //honda.Entries.Add(new FillupEntry
            //                      {
            //                          EntryDate = new DateTime(2008,01,11),
            //                          FillAmount = 15.735,
            //                          PricePerUnit = 3.099,
            //                          
            //                          OdometerReading = 23952,
            //                          OdometerUnit = honda.OdometerUnit,
            //                          PumpUnit = UnitOfMeasure.DefaultVolumeUnit
            //                      });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,01,12),
                                      FillAmount = 16.674,
                                      
                                      PricePerUnit = 2.999,
                                      OdometerReading = 24324,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,01,12).AddMilliseconds(20),
                                      FillAmount = 16.286,
                                      
                                      PricePerUnit = 3.099,
                                      OdometerReading = 24669,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,01,13),
                                      FillAmount = 12.096,
                                      
                                      PricePerUnit = 2.959,
                                      OdometerReading = 24997,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,01,17),
                                      FillAmount = 17.972,
                                      
                                      PricePerUnit = 2.949,
                                      OdometerReading = 25356,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,01,17).AddMilliseconds(20),
                                      FillAmount = 17.246,
                                      
                                      PricePerUnit = 2.899,
                                      OdometerReading = 25697,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,01,17).AddMilliseconds(40),
                                      FillAmount = 18.004,
                                      
                                      PricePerUnit = 2.899,
                                      OdometerReading = 26080,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,01,19),
                                      FillAmount = 16.18,
                                      
                                      PricePerUnit = 3.099,
                                      OdometerReading = 26428,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,01,20),
                                      FillAmount = 15.494,
                                      PricePerUnit = 2.999,
                                      
                                      OdometerReading = 26736,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,01,21),
                                      FillAmount = 15.077,
                                      PricePerUnit = 2.999,
                                      
                                      OdometerReading = 27029,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,02,20),
                                      FillAmount = 15.19,
                                      PricePerUnit = 2.959,
                                      
                                      OdometerReading = 27266,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,02,22),
                                      FillAmount = 16.251,
                                      
                                      PricePerUnit = 3.075,
                                      OdometerReading = 27539,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,03,17),
                                      FillAmount = 17.556,
                                      
                                      PricePerUnit = 3.299,
                                      OdometerReading = 27856,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,03,23),
                                      FillAmount = 17.16,
                                      
                                      PricePerUnit = 3.299,
                                      OdometerReading = 28207,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,04,05),
                                      FillAmount = 15.862,
                                      
                                      PricePerUnit = 3.339,
                                      OdometerReading = 28536,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,04,17),
                                      FillAmount = 7.199,
                                      PricePerUnit = 3.479,
                                      
                                      OdometerReading = 28680,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            honda.Entries.Add(new FillupEntry
                                  {
                                      EntryDate = new DateTime(2008,04,22),
                                      FillAmount = 16.8,
                                      
                                      PricePerUnit = 3.659,
                                      OdometerReading = 29017,
                                      OdometerUnit = honda.OdometerUnit,
                                      PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                  });
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,05,10), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,05,25), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,06,19), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,06,21), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,06,28), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,06,28).AddMilliseconds(20), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,07,03), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,07,17), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,07,23), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,08,23), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,10,11), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,10,22), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,11,12), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,11,16), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,11,23), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,11,29), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,11,30), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,12,09), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,12,17), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2008,12,19), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,01,02), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,01,04), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,01,09), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,01,16), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,01,23), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,02,10), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,02,22), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,02,25), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,03,10), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,03,14), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,03,18), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,03,25), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,03,30), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,04,11), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,04,12), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,05,01), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,05,17), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,05,30), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,06,13), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,06,15), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,06,29), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,07,01), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,07,07), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,07,09), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,07,12), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,07,19), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,07,30), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,08,07), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,08,08), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,08,20), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,09,21), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,09,29), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,10,04), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,10,04).AddMilliseconds(10), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,10,16), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,10,18), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,10,24), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,10,29), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,11,05), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,11,16), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,11,20), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,12,07), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,12,23), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,12,27), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2009,12,31), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,01,13), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,01,15), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,02,09), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,02,23), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,03,09), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,03,16), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,03,29), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,04,04), PreviousEntryMissed = true, FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,04,04).AddMilliseconds(20), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,04,15), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,04,16), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,04,22), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,04,28), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,05,08), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,05,15), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,05,31), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,06,15), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,06,19), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,06,25), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,07,03), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,07,03).AddMilliseconds(20), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,07,03).AddMilliseconds(40), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,07,21), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,07,23), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,07,29), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,07,31), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,07,31).AddMilliseconds(20), WasNotToppedOff = true, FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,08,02), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,08,02).AddMilliseconds(20), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,08,02).AddMilliseconds(40), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,08,06), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,08,07), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,08,14), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,08,19), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,08,21), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});
            //honda.Entries.Add(new FillupEntry { EntryDate = new DateTime(2010,08,28), FillAmount = , PricePerUnit = , OdometerDistanceUnit = honda.OdometerDistanceUnit, OdometerReading = , OdometerDistanceUnit = honda.OdometerUnit, PumpVolumeUnit = UnitOfMeasure.Volume.Gallon});

            foreach (FillupEntry entry in honda.Entries)
            {
                entry.PumpUnit = UnitOfMeasure.DefaultVolumeUnit;
            }
            return honda;
        }
    }
}