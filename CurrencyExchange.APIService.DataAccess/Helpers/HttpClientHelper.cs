namespace CurrencyExchange.APIService.DataAccess.Helpers
{
    public static class HttpClientHelper
    {
        public static string DoHttpRequest(string httpMethod, string baseURI, string requestParam, string data = "")
        {
            string responseString = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.ApiKey, Constants.ApiKeyValue);
                HttpRequestMessage? request = null;
                if (httpMethod == "GET")
                {
                    request = new HttpRequestMessage(HttpMethod.Get, requestParam);
                }
                if (httpMethod == "POST")
                {
                    request = new HttpRequestMessage(HttpMethod.Post, requestParam);
                    request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                }
                var response = client.SendAsync(request).GetAwaiter().GetResult();
                responseString = response.Content.ReadAsStringAsync().Result.ToString();
            }
            return responseString;
        }
    }
}
