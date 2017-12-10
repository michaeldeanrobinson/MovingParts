using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace MP.Models.Rest
{
    public static class ResultHandler
    {
        public static Error CreateError(string message, int number, ErrorLevel level, ErrorType type, string stackTrace = null, string exceptionTypeName = null)
        {
            return new Error
            {
                Code = new ErrorCode { Number = number, Type = type },
                Level = level,

                Message = message,
                Stack = stackTrace,
                Exception = exceptionTypeName,
            };
        }

        public static Error CreateError(Exception ex, int number, ErrorLevel level, ErrorType type)
        {
            return CreateError(ex.Message, number, level, type, ex.StackTrace, ex.GetType().Name);
        }

        public static Result CreateResultError(string message, int number, ErrorLevel level, ErrorType type)
        {
            return new Result { Error = CreateError(message, number, level, type) };
        }

        public static Result CreateResultError(Exception ex, int number, ErrorLevel level, ErrorType type)
        {
            return new Result { Error = CreateError(ex, number, level, type) };
        }

        public static Result<T> CreateResultError<T>(string message, int number, ErrorLevel level, ErrorType type)
        {
            return new Result<T> { Error = CreateError(message, number, level, type) };
        }

        public static Result<T> CreateResultError<T>(Exception ex, int number, ErrorLevel level, ErrorType type)
        {
            return new Result<T> { Error = CreateError(ex, number, level, type) };
        }

        public static Result<TResponseModel> CreateValidationResultError<TRequestModel, TResponseModel>(IEnumerable<ModelState> values, TRequestModel requestModel)
            where TRequestModel : IRequestModel
            where TResponseModel : IResponseModel
        {
            return new Result<TResponseModel>
            {
                ProcessTag = requestModel.ProcessTag,
                Error = CreateError(GetValidationMessage(values), 400, ErrorLevel.Data, ErrorType.Fatal),
            };
        }

        private static string GetValidationMessage(IEnumerable<ModelState> values)
        {
            return String.Join(" | ", values.SelectMany(v => v.Errors).Select(e => e.Exception?.Message ?? e.ErrorMessage));
        }
    }
}
