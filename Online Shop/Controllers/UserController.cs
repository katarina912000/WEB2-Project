using Microsoft.AspNetCore.Mvc;
using Online_Shop.DTO;
using Online_Shop.Interfaces;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace Online_Shop.Controllers
{
    public class UserController:ControllerBase
    {

        //kontrolerski sloj vidi samo dto, prosledjuje ga servisnom sloju, i od servisnog sloja dobija dto i od fronta
        private IUser userServis;

        public UserController(IUser userServis)
        {
            this.userServis = userServis;
        }

       
        [HttpPost("/registration")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm]UserRegistrationDTO urDto)
        {
            try
            {
                await   userServis.Register(urDto);

                return Ok(string.Format("Korisnik : {0} je uspesno registrovan na sistem!",urDto.UserName));
            }
            catch (Exception e)
            {
                //return BadRequest($"Greska: {e.InnerException?.Message}");
                return BadRequest(new { errors = new List<string> { e.Message } });
            }

        }
        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]UserLoginDTO urDto)
        {
            try
            {
                await userServis.Login(urDto);

                return Ok(string.Format("Korisnik je uspesno ulogovan na sistem!"));
            }
            catch (Exception e)
            {
                //return BadRequest($"Greska: {e.InnerException?.Message}");
                return BadRequest(new { errors = new List<string> { e.Message } });
            }
        }

        //dobavljanje liste usera koji imaju approved
        //[HttpGet("/verification")]
        //public async Task<IActionResult> Verification()
        //{
        //    try
        //    {
        //        await userServis.Login(urDto);

        //        return Ok(string.Format("Korisnik je uspesno ulogovan na sistem!"));
        //    }
        //    catch (Exception e)
        //    {
        //        //return BadRequest($"Greska: {e.InnerException?.Message}");
        //        return BadRequest(new { errors = new List<string> { e.Message } });
        //    }
        //}
    }
}
