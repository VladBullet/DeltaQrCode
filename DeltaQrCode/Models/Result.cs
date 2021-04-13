using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Models
{
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class Result<T> where T : class, new()
    {
        public bool Successful { get; private set; }

        public T Entity { get; private set; }

        public Exception Error { get; private set; }

        public string Message { get; private set; }

        public static Result<T> ResultOk(T obj, string message = null)
        {
            return new Result<T>
            {
                Successful = true,
                Entity = obj,
                Error = null,
                Message = message
            };
        }
        public static Result<T> ResultError(T obj, Exception er, string errorMessage = null)
        {
            if (string.IsNullOrEmpty(errorMessage))
                return new Result<T>
                {
                    Successful = false,
                    Entity = obj,
                    Error = er,
                    Message = errorMessage
                };
            return new Result<T>
            {
                Successful = false,
                Entity = obj,
                Error = new Exception(errorMessage, er),
                Message = errorMessage
            };
        }
        public static Result<T> ResultError(Exception er, string errorMessage = null)
        {
            if (string.IsNullOrEmpty(errorMessage))
                return new Result<T>
                {
                    Successful = false,
                    Entity = null,
                    Error = er,
                    Message = errorMessage
                };
            return new Result<T>
            {
                Successful = false,
                Entity = null,
                Error = new Exception(errorMessage, er),
                Message = errorMessage
            };
        }
    }

}
