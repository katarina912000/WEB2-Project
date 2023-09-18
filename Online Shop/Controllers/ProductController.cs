using Online_Shop.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Online_Shop.DTO;
using Microsoft.AspNetCore.Cors;

namespace Online_Shop.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    [EnableCors("ReactAppPolicy")]
    public class ProductController : ControllerBase
    {
      
            private readonly IProduct artikalServis;

            public ProductController(IProduct artikalServis)
            {
                this.artikalServis = artikalServis;
            }


            [HttpGet("sviArtikli")]
            [Authorize(Roles = "CUSTOMER")]
            public async Task<ActionResult<List<ProductDTO>>> GetArtikli()
            {
                try
                {
                    var artikli = await artikalServis.DobaviSveArtikle();
                    return Ok(artikli);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }


            [HttpGet("{id}")]
            [Authorize(Roles = "SELLER", Policy = "VerifikovanProdavac")]
            public async Task<ActionResult<ProductDTO>> GetArtikal(int id)
            {
                try
                {
                    var artikal = await artikalServis.DobaviArtikalpoID(id);

                    if (artikal == null)
                    {
                        return BadRequest("Artikal ne postoji");
                    }
                    return Ok(artikal);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }


            [HttpPut("{id}")]
            [Authorize(Roles = "SELLER", Policy = "VerifikovanProdavac")]
            public async Task<ActionResult<ProductDTO>> UpdateArikal(int id, [FromForm] AddProductDTO artikalDTO)
            {
                try
                {
                    var artikal = await artikalServis.AzurirajArtikal(id, artikalDTO);
                    if (artikal == null)
                    {
                        return BadRequest("Artikal ne postoji");
                    }
                    return Ok(artikal);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }



            [HttpDelete("{id}")]
            [Authorize(Roles = "SELLER", Policy = "VerifikovanProdavac")]
            [AllowAnonymous]
            public async Task<ActionResult<ProductDTO>> DeleteArtikal(int id)
            {
                try
                {
                    var obrisanArtikal = await artikalServis.ObrisiArtikal(id);
                    if (obrisanArtikal == null)
                    {
                        return BadRequest("Artikal ne postoji");
                    }
                    return Ok(obrisanArtikal);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException?.Message ?? ex.Message);
                }
            }



            [HttpPost("dodajArtikal")]
            [Authorize(Roles = "SELLER", Policy = "VerifikovanProdavac")]
            public async Task<IActionResult> DodajArtikal([FromForm] AddProductDTO artikalDTO)
            {
                try
                {
                    await artikalServis.DodajArtikal(artikalDTO);

                    return Ok(string.Format("Artikal : {0} je uspjesno dodat u bazu!", artikalDTO.Name));
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException?.Message ?? e.Message);
                }

            }

            //ovo prepraviti tako da prima id a ne body jer ne dozvoljava body, i skontati na frontu gde ovo pozivati
            [HttpGet("provera-dostupnosti")]
            [Authorize(Roles = "SELLER", Policy = "VerifikovanProdavac")]
            public async Task<ActionResult<bool>> ProvjeraDostupnostiArtikla(ProductDTO artikalDTO)
            {
                try
                {
                    var dostupan = await artikalServis.DaLiJeDostupanArtikal(artikalDTO);
                    return Ok(dostupan);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }



            [HttpGet("dobaviArtikleProdavca/{id}")]
            [Authorize(Roles = "SELLER", Policy = "VerifikovanProdavac")]
            public async Task<ActionResult<List<ProductDTO>>> GetArtikliProdavca(int id)
            {
                try
                {
                    var artikli = await artikalServis.DobaviSveArtikleProdavca(id);
                    return Ok(artikli);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

        }
    }

