using Demo.Mediator.Customers.Api.Models.Requests;
using FluentValidation;

namespace Demo.Mediator.Customers.Api.Validators
{
    public class GetCustomerByIdRequestValidator : ModelValidatorBase<GetCustomerByIdRequest>
    {
        public GetCustomerByIdRequestValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().NotEmpty();
        }
    }
}