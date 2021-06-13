using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.DataAccess.Commands;
using Demo.Mediator.Customers.Api.Extensions;
using FluentValidation;

namespace Demo.Mediator.Customers.Api.Validators
{
    public class CreateCustomerCommandValidator : ModelValidatorBase<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Name).IsRequired().WithErrorDetails(ErrorCodes.Required, ErrorMessages.Required);
            RuleFor(x => x.Address).IsRequired().WithErrorDetails(ErrorCodes.Required, ErrorMessages.Required);
        }
    }
}