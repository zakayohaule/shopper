using System;

namespace ShopperAdmin.Other
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
