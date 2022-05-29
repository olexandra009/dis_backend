using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using DIS_Server.Configuration;
using DIS_Server.DTO;
using DIS_Server.Models;
using DIS_Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DIS_Server.Controllers
{
    public class UserController : ControllerBase
    {
        protected readonly IMapper Mapper;
        protected readonly IUserService Service;
        protected readonly ISendEmailService EmailService;

        public UserController(IMapper mapper, IUserService service, ISendEmailService emailService)
        {
            Mapper = mapper;
            Service = service;
            EmailService = emailService;
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Token([FromBody] LoginUserDto user)
        {
            User userModel;
            ClaimsIdentity identity;
            try
            {
                (identity, userModel) = GetIdentity(user.Login, user.Password);
            }
            catch (AccessViolationException e)
            {
                return NotFound(new { errorText = e.Message });
            }

            if (identity == null)
            {
                return NotFound(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOption.ISSUER,
                audience: AuthOption.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOption.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOption.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var userDto = Mapper.Map<UserDto>(userModel);
            userDto.Password = "hidden";
            var response = new
            {
                access_token = encodedJwt,
                user = userDto,
            };

            return new JsonResult(response);
        }

        private (ClaimsIdentity claimsIdentity, User person) GetIdentity(string login, string password, bool hashed = false)
        {

            User person = Service.Get(login, password, hashed).Result;
            if (person != null)
            {
                if (!person.IsUserConfitmed) throw new AccessViolationException("Email is not confirm!");
                var roles = person.Role.Split(", ");
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim("person/user/identificate", person.Login),

                };
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
                }

                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return (claimsIdentity, person);
            }

            return (null, null);
        }


        [HttpPost("/registration")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Registration([FromBody] UserDto user)
        {
            user.IsUserConfitmed = true;
            var model = Mapper.Map<User>(user);
            var createdModel =  await Service.Create(model);
            if (createdModel == null) return Conflict($"user with login {user.Login} exists");
            var dto = Mapper.Map<UserDto>(createdModel);
            dto.Password = "hidden";
            //var code = HttpUtility.UrlEncode(createdModel.Login.GetHashCode().ToString());
            //var url = $"https://localhost:44365/#/confirm/{createdModel.Login}/{code}";
            //var url = "https://" + $"localhost:44365/confirm?id={createdModel.Login}&code={code}";
            //await EmailService.SendConfirmLetter(createdModel.Login, createdModel.Login, url);
            return Created("", dto);
        }

        [HttpGet("/confirm")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Confirm([FromQuery] string id, [FromQuery] string code)
        {
            var user = await Service.Get(id);
            if (HttpUtility.UrlEncode(user.Login.GetHashCode().ToString()) == code)
            {
                user.IsUserConfitmed = true;
                Service.Update(user);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("/isLogin")]
        [Authorize(Roles = "user,admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult IsLogin()
        {
            return Ok();
        }

    }
}
