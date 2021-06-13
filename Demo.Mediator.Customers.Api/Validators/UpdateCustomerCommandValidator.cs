using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.DataAccess.Commands;
using Demo.Mediator.Customers.Api.Extensions;

namespace Demo.Mediator.Customers.Api.Validators
{
    public class UpdateCustomerCommandValidator : ModelValidatorBase<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Id).IsRequired().WithErrorDetails(ErrorCodes.Required, ErrorMessages.Required);
        }
    }
}