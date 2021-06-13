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
            RuleFor(x => new[] {x.Name, x.Address, x.UserName})
                .AllAreRequired()
                .WithErrorDetails(ErrorCodes.AllAreRequired, ErrorMessages.AllAreRequired)
                .When(x=> x.Id.IsEmpty());
            
            RuleFor(x => new[] {x.Name, x.Address, x.UserName})
                .AnyIsRequired()
                .WithErrorDetails(ErrorCodes.AnyIsRequired, ErrorMessages.AnyIsRequired)
                .When(x=> !x.Id.IsEmpty());
        }
    }
}