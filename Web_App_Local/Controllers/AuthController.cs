using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Web_App_Local.Models;
using Web_App_Local.Services;

namespace Web_App_Local.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        CorAuthService authenticationService;

        public AuthController(CorAuthService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                var Iscreated = await authenticationService.RegisterUserAsync(user);
               if  (Iscreated == false)
                    {
                    return Conflict("The User Already Present");
                }
                var Responsedata = new ResponseData
                {
                    Message = $"{user.EMail} User created successfully"
                };
                return Ok(Responsedata);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginUser inputModel)
        {
            if (ModelState.IsValid)
            {
                var Token = await authenticationService.AuthinticateUserAsync(inputModel);
                if (Token == null)
                {
                    return Unauthorized("The authintication failed");
                }
                var Responsedata = new ResponseData
                {
                    Message = Token
                };
                return Ok(Responsedata);
            }
            return BadRequest(ModelState);
        }
    }
}
