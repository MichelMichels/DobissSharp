using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Configuration;

namespace MichelMichels.DobissSharp.Services
{
    public class DobissJwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly string _secretKey;
        private readonly string _applicationName;
        private readonly int _days;

        public DobissJwtTokenGenerator(IConfigurationService configurationService)
        {
            _applicationName = configurationService.Get("applicationName");
            _secretKey = configurationService.Get("secretKey");
            _days = configurationService.Get<int>("expirationDays");
        }

        public string GenerateToken()
        {
            var payload = new Dictionary<string, object>
            {
                { "name", _applicationName },
                { "exp", DateTimeOffset.UtcNow.AddDays(_days).ToUnixTimeSeconds() },
                { "iat", DateTimeOffset.UtcNow }
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, _secretKey);

            return $"Bearer {token}";
        }

    }
}
