using System;

namespace Petroledger.Exceptions
{
    internal class CorruptEntryException : Exception
    {
        public CorruptEntryException()
        {
        }

        public CorruptEntryException(string message) : base(message)
        {
        }

        public CorruptEntryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}