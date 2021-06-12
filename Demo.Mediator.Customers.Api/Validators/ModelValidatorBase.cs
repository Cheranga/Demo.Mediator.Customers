using Demo.Mediator.Customers.Api.Constants;
using FluentValidation;
using FluentValidation.Results;

namespace Demo.Mediator.Customers.Api.Validators
{
    public class ModelValidatorBase<TModel> : AbstractValidator<TModel>
    {
        protected ModelValidatorBase()
        {
            CascadeMode = CascadeMode.Stop;
        }

        protected override bool PreValidate(ValidationContext<TModel> context, ValidationResult result)
        {
            var instance = context.InstanceToValidate;

            if (instance != null)
            {
                return true;
            }

            result.Errors.Add(new ValidationFailure("", ErrorMessages.InstanceIsNull)
            {
                ErrorCode = ErrorCodes.InstanceIsNull
            });

            return false;
        }
    }
}