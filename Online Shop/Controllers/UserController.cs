using Microsoft.AspNetCore.Mvc;
using Online_Shop.DTO;
using Online_Shop.Interfaces;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace Online_Shop.Controllers
{
    [EnableCors("ReactAppPolicy")]
    public class UserController:ControllerBase
    {

        //kontrolerski sloj vidi samo dto, prosledjuje ga servisnom sloju, i od servisnog sloja dobija dto i od fronta
        private IUser userServis;

        public UserController(IUser userServis)
        {
            this.userServis = userServis;
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var korisnik = await userServis.GetUserById(id);

                if (korisnik == null)
                {
                    return BadRequest("Korisnik ne postoji");
                }
                return Ok(korisnik);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
               string token= await userServis.Login(urDto);

                return Ok(token);
            }
            catch (Exception e)
            {
                //return BadRequest($"Greska: {e.InnerException?.Message}");
                return BadRequest(new { errors = new List<string> { e.Message } });
            }
        }
        
        [HttpPost("/googleLogovanje")]

        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogin([FromForm] string googleToken)
        {
            try
            {
                string token = await userServis.GoogleLogovanje(googleToken);

                return Ok(string.Format("{0}", token));
            }
            catch (Exception e)
            {
                return BadRequest($"Greska: {e.InnerException?.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, [FromForm] UserUpdateDTO korisnikDto)
        {
            try
            
            {
                var updatedKorisnik = await userServis.UpdateUser( id,korisnikDto);
                if (updatedKorisnik == null)
                {
                    return BadRequest("Korisnik ne postoji");
                }
                return Ok(updatedKorisnik);
            }
            catch (Exception ex)
            {

                return BadRequest($"Greska: {ex.InnerException?.Message}");

            }
        }
        //napraviti kontroler za vracanje liste prodavaca sa verified false i status reject
        [HttpGet("/verificationSellers")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<List<UserDTO>>> GetAllSell()
        {
            try
            {
                var korisnici = await userServis.GetAllSeller();
                return Ok(korisnici);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //on ce se baviti primanjem podatka od fronta, tu stigne primljen ili ne

        [HttpPost("/send/{email}/{verifikacija}")]
        public async Task<ActionResult> Send(string email,string verifikacija)
        {
            try
            {
                 
                await userServis.SendMail(email, verifikacija);
                return Ok(string.Format("Uspesno poslato"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      
    }
}
