using RestSharp;
using RestSharp.Authenticators;

namespace MichelMichels.DobissSharp.Services
{
    public class DobissAuthenticator : AuthenticatorBase
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public DobissAuthenticator(IJwtTokenGenerator jwtTokenGenerator) : base("")
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        
        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            return await Task.Run(() =>
            {
                var token = _jwtTokenGenerator.GenerateToken();
                return new HeaderParameter(KnownHeaders.Authorization, token);
            });
        }
    }
}
