using System;

namespace Shared.Common
{
    public class OutOfStockException : Exception
    {
        public OutOfStockException()
        {
        }

        public OutOfStockException(string message) : base(message)
        {
        }
    }
}
