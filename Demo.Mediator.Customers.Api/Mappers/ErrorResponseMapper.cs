using System.Linq;
using AutoMapper;
using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.Models.Responses;
using FluentValidation.Results;

namespace Demo.Mediator.Customers.Api.Mappers
{
    public class ErrorResponseMapper : ITypeConverter<ValidationResult, ErrorResponse>
    {
        public ErrorResponse Convert(ValidationResult source, ErrorResponse destination, ResolutionContext context)
        {
            if (source == null)
            {
                return new ErrorResponse
                {
                    ErrorCode = ErrorCodes.InternalServerError,
                    ErrorMessage = ErrorMessages.InternalServerError
                };
            }

            var errorMessages = source.Errors.Select(x => new Error
            {
                ErrorCode = x.ErrorCode,
                Property = x.PropertyName,
                ErrorMessage = x.ErrorMessage
            }).ToList();

            return new ErrorResponse
            {
                ErrorCode = ErrorCodes.InvalidInput,
                ErrorMessage = ErrorMessages.InvalidInput,
                Errors = errorMessages
            };
        }
    }
}