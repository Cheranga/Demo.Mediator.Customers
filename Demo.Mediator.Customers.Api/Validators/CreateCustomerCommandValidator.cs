using Demo.Mediator.Customers.Api.DataAccess.Commands;
using FluentValidation;

namespace Demo.Mediator.Customers.Api.Validators
{
    public class CreateCustomerCommandValidator : ModelValidatorBase<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Address).NotNull().NotEmpty();
        }
    }
}