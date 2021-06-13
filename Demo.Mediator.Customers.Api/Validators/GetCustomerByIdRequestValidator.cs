using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.Extensions;
using Demo.Mediator.Customers.Api.Models.Requests;

namespace Demo.Mediator.Customers.Api.Validators
{
    public class GetCustomerByIdRequestValidator : ModelValidatorBase<GetCustomerByIdRequest>
    {
        public GetCustomerByIdRequestValidator()
        {
            RuleFor(x => x.CustomerId).IsRequired().WithErrorDetails(ErrorCodes.Required, ErrorMessages.Required);
        }
    }
}