using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Demo.Mediator.Customers.IntegrationTests.MockData
{
    public static class AssemblyResourceFileReader
    {
        public static async Task<TModel> GetFileContentAsync<TModel>(string fileName) where TModel:class
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return default;
            }

            try
            {
                var assembly = typeof(AssemblyResourceFileReader).Assembly;

                var testDataFileName = $"{typeof(AssemblyResourceFileReader).Assembly.GetName().Name}.MockData.{fileName}.json";
                using (var stream = assembly.GetManifestResourceStream(testDataFileName) /*assembly.GetManifestResourceStream($"AGL.Shared.Communications.Functions.UnitTests.MockData.{fileName}")*/)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var content = await reader.ReadToEndAsync();
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            return default;
                        }

                        var model = JsonConvert.DeserializeObject<TModel>(content, new JsonSerializerSettings
                        {
                            Error = (sender, args) => args.ErrorContext.Handled = true
                        });

                        return model;
                    }
                }
            }
            catch
            {
            }

            return default;
        }
    }
}