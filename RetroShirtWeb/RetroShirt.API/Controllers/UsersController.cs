using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using RetroShirt.API.Models;
using RetroShirt.Business.Abstract;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RetroShirt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
        private IDistributedCache distributedCache;

        public UsersController(IUserService userService,IDistributedCache distributedCache)
        {
            this.userService = userService;
            this.distributedCache = distributedCache;
        }

        [HttpGet("[action]")]
        public IActionResult GetProductsForDistrCache()
        {

            return Ok(distributedCache.GetString("myUsers"));

        }
        [HttpPost]
        public IActionResult Login(UserLoginModel loginModel)
        {
            var user = userService.GetUser(loginModel.Username,loginModel.Password);

            if (user != null)
            {
                //Step-1 : Claim bilgileri.
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName,user.Username), //claimi jwt ile de yazabilirsin.
                    new Claim(ClaimTypes.Role,user.Role),
                };

                //Step-2 : secret-key üretilmesi. unpredictable olması daha iyi.

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret key generating here.")); //utf-8 daha iyi daha global. 
                var credential=new SigningCredentials(key,SecurityAlgorithms.HmacSha256); // en düsük güvenliklisi bu algortima.

                //Step-3: token özelliklerinin tanımlanması ve üretilmesi.

                var token = new JwtSecurityToken(
                    issuer: "Turkcell", // issuer, kullanıcısı kim o. herhangi bir isim.
                    audience: "Turkcell", // claime ekliyor bunu. 
                    claims: claims,
                    notBefore:DateTime.Now,
                    expires:DateTime.Now.AddMinutes(20), // token 20 dklik oldu.
                    signingCredentials: credential
                    );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });


            }
            return BadRequest(new {message="Incorrect username or password."});

            //2.05.00

        }
    }
}
