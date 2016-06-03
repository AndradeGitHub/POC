using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIOAuth2.Controllers
{
    public class TesteOAuthController : ApiController
    {
        [HttpGet]
        [Authorize]
        public string GetData()
        {
            return "Testing...";
        }
    }
}