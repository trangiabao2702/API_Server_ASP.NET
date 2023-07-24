using API_Server.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Server.Dto
{
    public class Tokens
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
