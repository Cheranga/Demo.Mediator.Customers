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
        public const string EmptyData = "EMPTY_DATA";
        public const string Required = "REQUIRED";
        public const string AnyIsRequired = "ANY_OF_THESE_ARE_REQUIRED";
        public const string AllAreRequired = "ALL_OF_THESE_ARE_REQUIRED";
        public const string DataAccessError = nameof(DataAccessError);
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
        public const string EmptyData = "data not available";
        public const string Required = "this is required";
        public const string AnyIsRequired = "any of these are required";
        public const string AllAreRequired = "all of these are required";
        public const string DataAccessError = "error occurred when accessing data";
    }
}