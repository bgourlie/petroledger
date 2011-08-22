using System;

namespace Petroledger.Exceptions
{
    public class PetroledgerValidationException : Exception
    {
        public PetroledgerValidationException()
        {
        }

        public PetroledgerValidationException(string message) : base(message)
        {
        }

        public PetroledgerValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}