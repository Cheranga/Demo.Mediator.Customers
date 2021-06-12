using Demo.Mediator.Customers.Api.DataAccess.Queries;
using FluentValidation;

namespace Demo.Mediator.Customers.Api.Validators
{
    public class GetCustomerByIdQueryValidator : ModelValidatorBase<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}