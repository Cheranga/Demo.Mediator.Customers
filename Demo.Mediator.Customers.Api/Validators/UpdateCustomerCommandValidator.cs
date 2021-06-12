using Demo.Mediator.Customers.Api.DataAccess.Commands;
using FluentValidation;

namespace Demo.Mediator.Customers.Api.Validators
{
    public class UpdateCustomerCommandValidator : ModelValidatorBase<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x=>x.Id).NotNull().NotEmpty();
            RuleFor(x=>x.Name).NotNull().NotEmpty();
            RuleFor(x=>x.Address).NotNull().NotEmpty();
        }
    }
}