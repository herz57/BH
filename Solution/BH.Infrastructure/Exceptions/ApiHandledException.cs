using System;

namespace BH.Infrastructure.Exceptions
{
    public class ApiHandledException : Exception
    {
        public ApiHandledException(string msg) 
            : base(msg)
        {
        }
    }
}
