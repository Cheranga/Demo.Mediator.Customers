namespace Demo.Mediator.Customers.Api.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string data)
        {
            return string.IsNullOrEmpty(data?.Trim());
        }
    }
}