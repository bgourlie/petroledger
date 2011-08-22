using System;

namespace Petroledger.Exceptions
{
    public class DuplicateVehicleLabelException : Exception
    {
        public DuplicateVehicleLabelException()
        {
        }

        public DuplicateVehicleLabelException(string message) : base(message)
        {
        }
    }
}