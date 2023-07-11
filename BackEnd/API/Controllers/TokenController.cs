using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using SampleGeneratedCodeAPI.Models;
using SampleGeneratedCodeAPI.Utils;
using SampleGeneratedCodeDomain.Commons;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleGeneratedCodeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("api/token")]
        public async Task<ActionResult> GetToken(CreateTokenInputDto input)
        {
            HttpMapperResultUtil mapperResultUtil = new();
            OperationResultModel<CreateTokenOutputDto> result=new OperationResultModel<CreateTokenOutputDto>();
            string access_token;
            //in real life you have to do a real implementation
            await Task.Delay(500);

            if (input.User == "admin" && input.Password == "admin")
            {
                result.code = SampleGeneratedCodeDomain.Enums.OperationResultCodes.OK;
                result.message = "Generated Token";
                access_token = GenerateToken();
                result.payload = new CreateTokenOutputDto();
                result.payload.access_token = access_token;
                return mapperResultUtil.MapToActionResult(result);
            }
            else
            {
                result.code = SampleGeneratedCodeDomain.Enums.OperationResultCodes.FORBIDDEN;
                result.message = "Invalid credentials (use admin,admin for sample code)";
                return mapperResultUtil.MapToActionResult(result);
            }
        }

        private string GenerateToken()
        {
            string? Key = "";

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, "admin"),
                    new Claim(ClaimTypes.Name, "Super Administrator"),
                    new Claim(ClaimTypes.GivenName, $"Super Administrator DAmato"),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(ClaimTypes.Role, "superadmin"),
                    new Claim("scp", "api.access")
                };
            Key = _config["Jwt:Key"];
            if(Key is not null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(720),
                    signingCredentials: credentials);

                var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
                return jwt;
            }
            return "";
        }
    }
}
