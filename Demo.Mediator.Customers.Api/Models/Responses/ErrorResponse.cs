using System.Collections.Generic;

namespace Demo.Mediator.Customers.Api.Models.Responses
{
    public class Error
    {
        public string Property { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<Error> Errors { get; set; }

        public ErrorResponse()
        {
            Errors = new List<Error>();
        }
    }
}