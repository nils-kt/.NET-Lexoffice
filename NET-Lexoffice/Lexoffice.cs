namespace NET_Lexoffice
{
    public class Lexoffice
    {
        private readonly string _apiKey;
        public readonly Contacts Contacts;

        public Lexoffice(string apiKey)
        {
            _apiKey = apiKey;
            Contacts = new Contacts(apiKey);
        }
    }
}