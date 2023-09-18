using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Interfaces;
using Online_Shop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Online_Shop.Models;
using Online_Shop.DTO;

namespace Online_Shop.Controllers
{
    public class OrderController:ControllerBase
    {
        private readonly IOrder porudzbinaServis;
        public OrderController(IOrder porudzbinaServis)
        {
            this.porudzbinaServis = porudzbinaServis;
        }

        [HttpGet("svePorudzbine")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<List<Order>>> GetPorudzbine()
        {
            try
            {
                var porudzbine = await porudzbinaServis.DobaviSvePorudzbine();
                return Ok(porudzbine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("dodajPorudzbinu")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> DodajPorudzbinu([FromBody] AddOrderDTO porDTO)
        {
            try
            {
                int idPorudzbine = await porudzbinaServis.DodajPoruzbinu(porDTO);

                return Ok(idPorudzbine);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }

        }
    }
}
