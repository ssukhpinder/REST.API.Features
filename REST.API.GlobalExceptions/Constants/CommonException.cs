using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REST.API.GlobalExceptions.Constants
{
    public static class CommonException
    {
        public static string SomeUnknownError = "Some unknown error occured.";
        public static string CustomSomeUnknownError = "Custom Exception from REST.API.Features: Some unknown error occured.";
        public static string CustomExceptionPrefix = "Custom Exception from REST.API.Features ";
    }
}
