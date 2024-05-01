using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace MichelMichels.DobissSharp.Api.Services;

public class DobissJwtTokenGenerator(string applicationName) : IJwtTokenGenerator
{
    private readonly string applicationName = applicationName ?? throw new ArgumentNullException(nameof(applicationName));

    public string ApplicationName => applicationName;

    public string GenerateToken(string secretKey, int expiryDayCount = 7)
    {
        var payload = new Dictionary<string, object>
        {
            { "name", applicationName },
            { "exp", DateTimeOffset.UtcNow.AddDays(expiryDayCount).ToUnixTimeSeconds() },
            { "iat", DateTimeOffset.UtcNow }
        };

        IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
        IJsonSerializer serializer = new JsonNetSerializer();
        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

        return encoder.Encode(payload, secretKey);
    }
}
