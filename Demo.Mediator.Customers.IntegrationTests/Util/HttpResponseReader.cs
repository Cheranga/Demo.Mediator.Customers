using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Demo.Mediator.Customers.IntegrationTests.Util
{
    public class HttpResponseReader
    {
        public static async Task<TModel> Get<TModel>(HttpResponseMessage httpResponse) where TModel:class
        {
            var content = await httpResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(content))
            {
                return default(TModel);
            }

            var model = JsonConvert.DeserializeObject<TModel>(content, new JsonSerializerSettings
            {
                Error = (sender, args) => args.ErrorContext.Handled = true
            });

            return model;
        }
    }
}