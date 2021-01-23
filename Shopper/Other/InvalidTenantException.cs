using System;

namespace Shopper.Common
{
    public class InvalidTenantException : Exception
    {
        public InvalidTenantException()
        {
        }

        public InvalidTenantException(string message) : base(message)
        {
        }
    }
}
