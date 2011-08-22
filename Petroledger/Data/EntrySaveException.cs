using System;

namespace Petroledger.Data
{
    public class EntrySaveException : Exception
    {
        public EntrySaveException(string message) : base(message)
        {
        }
    }
}