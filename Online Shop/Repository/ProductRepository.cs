using Microsoft.EntityFrameworkCore;
using Online_Shop.DataBaseContext;
using Online_Shop.InterfaceRepository;
using Online_Shop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.Repository
{
    public class ProductRepository : IProductRepo
    {

        private readonly UserDbContext dbContext;
        public ProductRepository(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //
        public async Task<Product> AzurirajArtikal(Product artikal)
        {
            dbContext.Update(artikal);
            await dbContext.SaveChangesAsync();
            return artikal;
        }
        //
        public async Task<bool> DaLiJeDostupanArtikal(Product artikalDTO)
        {
            var artikal = await dbContext.TableProducts.FirstOrDefaultAsync(a => a.Name == artikalDTO.Name);


            if (artikal != null && artikal.Quantity > 0)
            {
                return true;  //dostupan
            }
            else
            {
                return false; //nije dostupan
            }
        }
        //
        public async Task<Product> DobaviArtikalpoID(int id)
        {
            var artikal = await dbContext.TableProducts.FirstOrDefaultAsync(a => a.Id == id);

            if (artikal == null)
            {
                return null;
            }
            return artikal;
        }
        //
        public async Task<List<Product>> DobaviSveArtikle()
        {
            return await dbContext.TableProducts.ToListAsync();

        }
        //
        public async Task<List<Product>> DobaviSveArtikleProdavca(int idProdavca)
        {
            return await dbContext.TableProducts
           .Where(art => art.KorisnikID == idProdavca)
           .ToListAsync();
        }
        //
        public async Task DodajArtikal(Product artikal)
        {
            if (artikal != null)
            {

                dbContext.TableProducts.Add(artikal);
                await dbContext.SaveChangesAsync();
            }
        }
        //
        public async Task<Product> ObrisiArtikal(int id)
        {
            var artikal = dbContext.TableProducts.FirstOrDefault(a => a.Id == id);

            if (artikal == null)
            {
                return null;
            }

            dbContext.Remove(artikal);
            await dbContext.SaveChangesAsync();
            return artikal;
        }
    }
}
