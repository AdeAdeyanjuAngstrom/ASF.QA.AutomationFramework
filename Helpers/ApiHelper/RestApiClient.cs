using System.Text;
using Newtonsoft.Json;

namespace AutomationFramework.Helpers.ApiHelper
{
    internal static class RestApiClient
    {
        private static readonly HttpClient HttpClient = new();

        public static async Task<string?> SendPostRequestAsync(string apiUrl, object requestData) //where T : new()
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await HttpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static async Task<string?> SendGetRequestAsync(string apiUrl)
        {
            try
            {
                var response = await HttpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
