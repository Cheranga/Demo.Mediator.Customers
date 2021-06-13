namespace Demo.Mediator.Customers.Api.Constants
{
    public class ErrorCodes
    {
        public const string CustomerExists = "CUSTOMER_EXISTS";
        public const string CustomerNotFound = "CUSTOMER_NOT_FOUND";
        public const string InstanceIsNull = "INSTANCE_IS_NULL";
        public const string InvalidInput = "INVALID_INPUT";
        public const string InternalServerError = "INTERNAL_SERVER_ERROR";
        public const string CreateCustomerError = "CREATE_CUSTOMER_ERROR";
        public const string UpdateCustomerError = "UPDATE_CUSTOMER_ERROR";
    }

    public class ErrorMessages
    {
        public const string CustomerExists = "customer exists";
        public const string CustomerNotFound = "customer not found";
        public const string InstanceIsNull = "instance is null";
        public const string InvalidInput = "invalid input";
        public const string InternalServerError = "internal server error";
        public const string CreateCustomerError = "error occurred when creating the customer";
        public const string UpdateCustomerError = "error occurred when updating the customer";
    }
}