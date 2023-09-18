using AutoMapper;
using Microsoft.AspNetCore.Http;
using Online_Shop.DataBaseContext;
using Online_Shop.DTO;
using Online_Shop.InterfaceRepository;
using Online_Shop.Interfaces;
using Online_Shop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.Services
{
    public class OrderService : IOrder
    {
        private readonly IMapper maper;
        private readonly IOrderRepo porudzbinaRepozitorijum;
        private readonly IProductRepo artikalRepozitorijum;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserRepo korisnikRepozitorijum;
        private readonly IItemRepo stavkaRepozitorijum;
        private readonly UserDbContext dbContext;

        public OrderService(IMapper map, IOrderRepo porudzbinaRepozitorijum, IUserRepo korisnikRepozitorijum, IHttpContextAccessor http, IProductRepo artikalRepozitorijum, IItemRepo stavkaRepozitorijum, UserDbContext context)
        {
            maper = map;
            this.porudzbinaRepozitorijum = porudzbinaRepozitorijum;
            this.artikalRepozitorijum = artikalRepozitorijum;
            httpContextAccessor = http;
            this.korisnikRepozitorijum = korisnikRepozitorijum;
            this.stavkaRepozitorijum = stavkaRepozitorijum;
            dbContext = context;
        }
        public Task<OrderDTO> AzurirajPorudzbinu(int id, OrderDTO porudzbinaDTO)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Product>> DobaviArtiklePorudzbine(int idPorudzbine)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Product>> DobaviArtiklePorudzbineProdavac(int idPorudzbine)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Order> DobaviPorudzbinuPoID(int id)
        {
            var porudzbina = await porudzbinaRepozitorijum.DobaviPorudzbinuPoID(id);
            if (porudzbina == null)
            {
                throw new Exception("Ne postoji u bazi");
            }

            return porudzbina;
        }

        public Task<List<Order>> DobaviPrethodnePorudzbineZaKupca(int id)
        {
            throw new System.NotImplementedException();
        }
        //
        public async Task<List<Order>> DobaviSvePorudzbine()
        {
            var porudzbine = await porudzbinaRepozitorijum.DobaviSvePorudzbine();
            if (porudzbine == null)
            {
                throw new Exception("Nema porudzbina u bazi");
            }
            return porudzbine;
        }

        public Task<List<Order>> DobaviSvePorudzbineZKupca(int kupacID)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> DodajPoruzbinu(AddOrderDTO dodajPorudzbinuDTO)
        {
            var idClaim = httpContextAccessor.HttpContext.User.FindFirst("UserID");

            if (idClaim == null)
            {
                throw new Exception("Korisnik ne postoji u klajmu");
            }

            int id;
            if (!int.TryParse(idClaim.Value, out id))
            {
                throw new Exception("ID nije konvertovan u broj");
            }

            if (string.IsNullOrEmpty(dodajPorudzbinuDTO.Address))
            {
                throw new Exception("Polje adresa je obavezno!");
            }

            var korisnik = await korisnikRepozitorijum.GetUserById(id);

            var porudzbina = maper.Map<Order>(dodajPorudzbinuDTO);

            //porudzbina.Id += 2;
            porudzbina.UserId = id;
            porudzbina.StatusOrder = StatusOrder.IN_PROGRESS;
            porudzbina.DeliveryTime = DateTime.Now;
           // porudzbina. = DateTime.Now.AddHours(1).AddMinutes(new Random().Next(60));
            porudzbina.User = korisnik;


            foreach (var stavkaDTO in dodajPorudzbinuDTO.Items)
            {
                var product = await artikalRepozitorijum.DobaviArtikalpoID(stavkaDTO.ArtikalStavkaID);

                if (stavkaDTO.Quantity > product.Quantity)
                {
                    throw new Exception("Nedovoljna kolicina artikla na stanju");
                }

                var stavka1 = new Item
                {
                    OrderId = porudzbina.Id,
                    ProductId = product.Id,
                    Order = porudzbina,
                    Product = product,
                    Quantity = stavkaDTO.Quantity
                };

                var existingStavka = porudzbina.Items.FirstOrDefault(s => s.OrderId == stavka1.OrderId && s.ProductId == stavka1.ProductId);

                if (existingStavka == null)
                {

                    try
                    {
                        porudzbina.Items.Add(stavka1);
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                        {
                            string s = e.InnerException.Message;
                        }
                    }
                }

                //300 je cijena dostave
                product.Quantity -= stavkaDTO.Quantity;
                porudzbina.Price += stavkaDTO.Quantity * product.Price + 300;

            }

            await porudzbinaRepozitorijum.DodajPoruzbinu(porudzbina);
            return porudzbina.Id;
        }

        public Task<List<Order>> MojePorudzbineProd(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Order>> NovePorudzineProdavca(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<OrderDTO> ObrisiPorudzbina(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task OtkaziPorudzbinu(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
