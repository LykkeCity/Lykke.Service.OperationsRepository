using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.OperationsRepository.Models
{
    public class ErrorResponse
    {
        public string ErrorMessage;

        public static ErrorResponse Create(string message)
        {
            var response = new ErrorResponse {ErrorMessage = message};

            return response;
        }

        public static ErrorResponse InvalidParameter(string parameterName)
        {
            var response = ErrorResponse.Create($"Invalid parameter value: {parameterName}");

            return response;
        }
    }
}
