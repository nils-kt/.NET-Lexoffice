#region Usings

using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;

#endregion

namespace NET_Lexoffice
{
    public class HTTPClient
    {
        private readonly string _apiKey;

        public HTTPClient(string apiKey)
        {
            _apiKey = apiKey;
        }


        public async Task<string> Send(int type, string path, params Parameter[] parameters)
        {
            RestClient client = new RestClient($"https://api.lexoffice.io/v1/{path}")
            {
                Timeout = -1
            };

            RestRequest request = null;

            switch (type)
            {
                case 0:
                    request = new RestRequest(Method.GET);
                    break;
                case 1:
                    request = new RestRequest(Method.POST);
                    break;
                case 2:
                    request = new RestRequest(Method.PUT);
                    break;
            }

            if (request != null)
            {
                request.AddHeader("Authorization", $"Bearer {_apiKey}");
                request.AddHeader("Accept", "application/json");

                if (parameters != null)
                    foreach (Parameter parameter in parameters)
                        request.AddParameter(parameter);

                IRestResponse response = await client.ExecuteAsync(request);
                if (response.IsSuccessful)
                    return response.Content;
                return JObject.Parse(response.Content).SelectToken("message").Value<string>();
            }

            throw new SystemException("request is null in HTTPClient");
        }
    }
}