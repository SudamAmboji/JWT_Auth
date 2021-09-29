using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_Auth.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JWT_AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public JWT_AuthController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }
        // GET: api/<JWT_AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<JWT_AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred) 
        {
            var token = _jwtAuthenticationManager.Authenticate(userCred.Username, userCred.Password);
            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
