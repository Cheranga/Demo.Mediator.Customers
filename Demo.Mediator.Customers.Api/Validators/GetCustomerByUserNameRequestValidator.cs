using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.Extensions;
using Demo.Mediator.Customers.Api.Models.Requests;

namespace Demo.Mediator.Customers.Api.Validators
{
    public class GetCustomerByUserNameRequestValidator : ModelValidatorBase<GetCustomerByUserNameRequest>
    {
        public GetCustomerByUserNameRequestValidator()
        {
            RuleFor(x => x.UserName).IsRequired().WithErrorDetails(ErrorCodes.Required, ErrorMessages.UserNameIsRequired);
        }
    }
}