using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config){
            _config = config;
            var signingKey = _config["JWt:SigningKey"];
            if (string.IsNullOrEmpty(signingKey)){
                throw new ArgumentNullException(nameof(signingKey), "Sgning key cannot be null or empty.");
            }

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        }
    }
}