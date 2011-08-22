using System;

namespace Petroledger.Exceptions
{
    public class DuplicateEntryDateTimeException : Exception
    {
        public DuplicateEntryDateTimeException()
        {
        }

        public DuplicateEntryDateTimeException(string message) : base(message)
        {
        }

        public DuplicateEntryDateTimeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}