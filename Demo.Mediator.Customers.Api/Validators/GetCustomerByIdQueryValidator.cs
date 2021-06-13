using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using Demo.Mediator.Customers.Api.Extensions;

namespace Demo.Mediator.Customers.Api.Validators
{
    public class GetCustomerByIdQueryValidator : ModelValidatorBase<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(x => x.Id).IsRequired().WithErrorDetails(ErrorCodes.Required, ErrorMessages.Required);
        }
    }
}