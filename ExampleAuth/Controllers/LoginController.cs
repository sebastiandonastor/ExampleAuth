using ExampleAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExampleAuth.Controllers
{
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("api/Login/Authenticate")]
        public IHttpActionResult Authenticate(LoginRequest user)
        {
            if (user == null) return BadRequest();

            if (user.Password == "12345")
            {
                var token = TokenGenerator.GenerateTokenJwt(user.Username);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }

        }
    }
}
