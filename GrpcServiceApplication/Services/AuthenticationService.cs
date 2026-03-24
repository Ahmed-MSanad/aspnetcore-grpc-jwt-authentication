using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcCRUDApplication.Protos;
using Microsoft.IdentityModel.Tokens;

namespace GrpcCRUDApplication.Services
{
    public class AuthenticationService(IConfiguration configuration) : AuthProtoService.AuthProtoServiceBase
    {
        public override async Task<CreateIdentityResponse> GenerateToken(Empty request, ServerCallContext context)
        {
            var expiration = DateTime.UtcNow.AddHours(1);

            Claim[] claims = [new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString())];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSecret")["Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7218",
                audience: "https://localhost:7218",
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            var _token = new JwtSecurityTokenHandler().WriteToken(token);

            await Task.Delay(100);

            return new CreateIdentityResponse { Token = _token };
        }
    }
}
