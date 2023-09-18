using Microsoft.EntityFrameworkCore;
using Online_Shop.DataBaseContext;
using Online_Shop.InterfaceRepository;
using Online_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.Repository
{
    public class OrderRepository : IOrderRepo
    {
        private readonly UserDbContext dbContext;
        private readonly IItemRepo stavkaRepozitorijum;
        public OrderRepository(UserDbContext dbContextm, IItemRepo stavkaRepozitorijum)
        {
            dbContext = dbContextm;
            this.stavkaRepozitorijum = stavkaRepozitorijum;
        }
        public Task<Order> AzurirajPorudzbinu(Order porudzbina)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Product>> DobaviArtiklePorudzbine(int idPorudzbine)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Product>> DobaviArtiklePorudzbineProdavac(int idPorudzbine, int idProdavca)
        {
            throw new System.NotImplementedException();
        }
        //
        public async Task<Order> DobaviPorudzbinuPoID(int id)
        {
            var porudzbina = await dbContext.TableOrders.FirstOrDefaultAsync(p => p.Id == id);

            if (porudzbina == null)
            {
                return null;
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
            return await dbContext.TableOrders.ToListAsync();
        }
        //
        public async Task DodajPoruzbinu(Order porudzbina)
        {
            if (porudzbina != null)
            {

                dbContext.TableOrders.Add(porudzbina);
                await dbContext.SaveChangesAsync();
            }
        }
        //
        public async Task<List<Order>> MojePorudzbine(int id)
        {
            var svePorudzbine = await dbContext.TableOrders.ToListAsync();

            return svePorudzbine;
        }
        //
        public async Task<List<Order>> NovePorudzineProdavca(int id)
        {
            var svePorudzbineProdavca = await dbContext.TableOrders
            .Where(p => p.StatusOrder != StatusOrder.CANCELLED)
            .ToListAsync();

            return svePorudzbineProdavca;
        }

        public Task<Order> ObrisiPorudzbina(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task OtkaziPorudzbinu(int id)
        {
            throw new System.NotImplementedException();
        }
        // da li ovde ubaciti id
        public  async Task<List<Order>> SvePorudzbineProdavca(int id)
        {
            var porudzbine = await DobaviSvePorudzbine();


            if (porudzbine == null)
            {
                throw new Exception("Ne postoji nijedna porudzbina u bazi");
            }
            return porudzbine;
        }
    }
}
