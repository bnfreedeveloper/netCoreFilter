using filterApp.TokenAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace filterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ITokenManager _tokenM;
        public AuthenticateController(ITokenManager tokenmanager)
        {
            this._tokenM = tokenmanager;
        }
     [HttpPost]
     public async Task<IActionResult> Authenticate(string userName, string password)
        {
            if (_tokenM.Authenticate(userName, password))
            {
                return Ok(new
                {
                    Token = _tokenM.CreateToken()
                });
            }
            return Unauthorized(new
            {
                error = "you don't have the authorization"
            });
        }
    }
}
