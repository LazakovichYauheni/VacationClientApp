namespace vacation.core
{
    public static class OAuth
    {
        public const string ResourceId = "VTS-Server-v2";
        public const string ClientId = "VTS-Mobile-v1";
        public const string ClientSecret = "VTS123456789";
        public const string TokenUrl = "https://vts-token-issuer-v2.azurewebsites.net/connect/token";
        public const string BaseUri = "https://vts-v2.azurewebsites.net";
        public const string VacationsUrl = "https://vts-v2.azurewebsites.net/api/vts/workflow";
    }
}
