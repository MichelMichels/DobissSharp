namespace MichelMichels.DobissSharp.Api.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(string secretKey, int expiryDayCount = 7);
}
