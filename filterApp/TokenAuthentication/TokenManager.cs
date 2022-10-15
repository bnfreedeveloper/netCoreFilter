using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace filterApp.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private List<Token> validTokens = new List<Token>();
        public bool Authenticate(string userName, string password)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password) && userName.ToLower()
                == "admin" && password == "admin") return true;
            return false;
        }
        public Token CreateToken()
        {
            var token = new Token
            {
                Value = Guid.NewGuid().ToString(),
                ExpiryDate = DateTime.Now.AddMinutes(1)
            };
            
            validTokens.Add(token);
            return token;
        }
        public bool VerifyToken(string token)
        {
            var result = validTokens.FirstOrDefault(x => x.Value == token);
            if (result != null)
            {
                if (result.ExpiryDate < DateTime.Now) return true;
            }
            return false;
        }
    }
}
