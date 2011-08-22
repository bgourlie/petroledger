using System;

namespace Petroledger.Exceptions
{
    public class NotEnoughSampleDataException : Exception
    {
        public NotEnoughSampleDataException()
        {
        }

        public NotEnoughSampleDataException(string message) : base(message)
        {
        }
    }
}