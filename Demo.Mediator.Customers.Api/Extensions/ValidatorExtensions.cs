using FluentValidation;

namespace Demo.Mediator.Customers.Api.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> IsRequired<T>(this IRuleBuilderInitial<T, string> ruleBuilderOptions)
        {
            return ruleBuilderOptions.Must(x => !x.IsEmpty());
        }
        
        public static IRuleBuilderOptions<T, object> WithErrorDetails<T>(this IRuleBuilderOptions<T, object> ruleBuilderOptions, string errorCode, string errorMessage)
        {
            return ruleBuilderOptions.WithErrorCode(errorCode).WithMessage(errorMessage);
        }
    }
}