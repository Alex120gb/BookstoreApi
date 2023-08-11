using BookstoreApi.Common;
using BookstoreApi.Services.Interfaces;

namespace BookstoreApi.Services
{
    public class JwtTokenGeneratorService : IJwtTokenGeneratorService
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public JwtTokenGeneratorService(JwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public string GenerateJwtTokentest(string username)
        {
            return _jwtTokenGenerator.GenerateJwtToken(username);
        }
    }
}
