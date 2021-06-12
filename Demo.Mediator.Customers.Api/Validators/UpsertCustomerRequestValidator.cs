using Demo.Mediator.Customers.Api.Models.Requests;
using FluentValidation;

namespace Demo.Mediator.Customers.Api.Validators
{
    public class UpsertCustomerRequestValidator : ModelValidatorBase<UpsertCustomerRequest>
    {
        public UpsertCustomerRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Address).NotNull().NotEmpty();
        }
    }
}