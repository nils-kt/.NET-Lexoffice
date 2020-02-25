using System.Threading.Tasks;
using RestSharp;

namespace NET_Lexoffice
{
    public class Contacts
    {
        private readonly string _apiKey;
        private readonly HTTPClient httpClient;

        internal Contacts(string apiKey)
        {
            _apiKey = apiKey;
            httpClient = new HTTPClient(apiKey);
        }

        public async Task<string> GetAllContacts(int page = 0)
        {
            return await httpClient.Send(0, $"contacts/?page={page}", null);
        }
    }
}