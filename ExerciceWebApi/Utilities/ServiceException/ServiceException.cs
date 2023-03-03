using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ExerciceWebApi.Utilities.ServiceException
{
    [Serializable]
    public class ServiceException : Exception
    {

        public string ErrorMessage { get; }

        public ServiceException(string? message, string errorMessage) : base(message)
        {
            ErrorMessage = errorMessage;
        }
        public ServiceException(string? message) : base(message)
        {
        }

        public ServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }




    }
}