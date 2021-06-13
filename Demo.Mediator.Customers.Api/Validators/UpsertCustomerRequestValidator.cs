using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.Extensions;
using Demo.Mediator.Customers.Api.Models.Requests;
using FluentValidation;

namespace Demo.Mediator.Customers.Api.Validators
{
    public class UpsertCustomerRequestValidator : ModelValidatorBase<UpsertCustomerRequest>
    {
        public UpsertCustomerRequestValidator()
        {
            RuleFor(x => x.Name).Must((request, s) => request.Id.IsEmpty() ? !s.IsEmpty() : true).WithErrorCode(ErrorCodes.EmptyData);
            RuleFor(x=>x.UserName).Must((request, s) => request.Id.IsEmpty() ? !s.IsEmpty() : true).WithErrorCode(ErrorCodes.EmptyData);
            RuleFor(x => x.Address).Must((request, s) => request.Id.IsEmpty() ? !s.IsEmpty() : true).WithErrorCode(ErrorCodes.EmptyData);
        }
    }
}