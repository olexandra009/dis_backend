using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DIS_Server.Controllers
{
    public class UserController : ControllerBase
    {
        [HttpPost("/login")]
        public IActionResult Token([FromBody] LoginUserDtO user)
        {
            
        }
    }
}
