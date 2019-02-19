using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExampleAuth.Controllers
{
    [Authorize]
    public class CoffeController : ApiController
    {
        public IHttpActionResult getCoffes()
        {
            return Ok(new[] { new { name = " Black Coffe", type = "Yummy Yummy" }, new { name = " Tea", type = "Not Yummy" } });
        }
    }
}
