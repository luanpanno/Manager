using Manager.Api.ViewModels;
using System.Collections.Generic;

namespace Manager.Api.Utilities
{
    public static class Responses
    {
        public static ResultViewModel ApplicationErrorMessage()
        {
            return new ResultViewModel
            {
                Message = "Internal Error",
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel ApplicationErrorMessage(string message)
        {
            return new ResultViewModel
            {
                Message = "Internal Error",
                Success = false,
                Data = message
            };
        }

        public static ResultViewModel DomainErrorMessage(string message)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel DomainErrorMessage(string message, IReadOnlyCollection<string> errors)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = false,
                Data = errors
            };
        }

        public static ResultViewModel UnauthorizedErrorMessage()
        {
            return new ResultViewModel
            {
                Message = "Login/Password invalid",
                Success = false,
                Data = null
            };
        }
    }
}
