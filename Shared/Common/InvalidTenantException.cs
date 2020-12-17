using System;

namespace Shared.Common
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
