using FluentValidation.Results;

namespace Demo.Mediator.Customers.Api.Core
{
    public class Result
    {
        public string ErrorCode { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public bool Status => string.IsNullOrEmpty(ErrorCode);

        public static Result Failure(string errorCode, string errorMessage)
        {
            return Failure(errorCode, new ValidationResult(new[]
            {
                new ValidationFailure("", errorMessage)
                {
                    ErrorCode = errorCode
                }
            }));
        }

        public static Result Failure(string errorCode, ValidationResult validationResult)
        {
            return new()
            {
                ErrorCode = errorCode,
                ValidationResult = validationResult
            };
        }

        public static Result Success()
        {
            return new();
        }
    }

    public class Result<TData>
    {
        public string ErrorCode { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public TData Data { get; set; }

        public bool Status => string.IsNullOrEmpty(ErrorCode);

        public static Result<TData> Failure(string errorCode, string errorMessage)
        {
            return Failure(errorCode, new ValidationResult(new[]
            {
                new ValidationFailure("", errorMessage)
                {
                    ErrorCode = errorCode
                }
            }));
        }

        public static Result<TData> Failure(string errorCode, ValidationResult validationResult)
        {
            return new()
            {
                ErrorCode = errorCode,
                ValidationResult = validationResult
            };
        }

        public static Result<TData> Success(TData data)
        {
            return new()
            {
                Data = data
            };
        }
    }
}