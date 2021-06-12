namespace Demo.Mediator.Customers.Api.Constants
{
    public class ErrorCodes
    {
        public const string CustomerExists = "CUSTOMER_EXISTS";
        public const string CustomerNotFound = "CUSTOMER_NOT_FOUND";
        public const string InstanceIsNull = "INSTANCE_IS_NULL";
        public const string InvalidInput = "INVALID_INPUT";
        public const string InternalServerError = "INTERNAL_SERVER_ERROR";
    }

    public class ErrorMessages
    {
        public const string CustomerExists = "customer exists";
        public const string CustomerNotFound = "customer not found";
        public const string InstanceIsNull = "instance is null";
        public const string InvalidInput = "invalid input";
        public const string InternalServerError = "internal server error";
    }
}