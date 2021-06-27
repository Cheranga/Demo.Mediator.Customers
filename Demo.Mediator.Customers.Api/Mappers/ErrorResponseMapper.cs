using System;
using System.Linq;
using AutoMapper;
using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.Models.Responses;

namespace Demo.Mediator.Customers.Api.Mappers
{
    public class ErrorResponseMapper<TData> : ITypeConverter<Result<TData>, ErrorResponse>
    {
        public ErrorResponse Convert(Result<TData> source, ErrorResponse destination, ResolutionContext context)
        {
            if (source == null)
            {
                return new ErrorResponse
                {
                    ErrorCode = ErrorCodes.InternalServerError,
                    ErrorMessage = ErrorMessages.InternalServerError
                };
            }

            if (source.Status)
            {
                throw new InvalidOperationException("Cannot create error responses for successful operations");
            }

            var errorMessages = source.ValidationResult.Errors.Select(x => new Error
            {
                ErrorCode = x.ErrorCode,
                ErrorMessage = x.ErrorMessage
            }).ToList();

            return new ErrorResponse
            {
                ErrorCode = source.ErrorCode,
                Errors = errorMessages
            };
        }
    }
}