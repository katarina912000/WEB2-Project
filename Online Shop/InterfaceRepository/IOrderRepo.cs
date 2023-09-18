using Online_Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Shop.InterfaceRepository
{
    public interface IOrderRepo
    {
        Task<List<Order>> DobaviSvePorudzbine();
        Task<Order> DobaviPorudzbinuPoID(int id);
        Task DodajPoruzbinu(Order porudzbina);
        Task<Order> AzurirajPorudzbinu(Order  porudzbina);
        Task<Order> ObrisiPorudzbina(int id);
        Task<List<Order>> DobaviPrethodnePorudzbineZaKupca(int id);
        Task OtkaziPorudzbinu(int id);
        Task<List<Order>> NovePorudzineProdavca(int id);
        Task<List<Order>> SvePorudzbineProdavca(int id);
        Task<List<Order>> MojePorudzbine(int id);
        Task<List<Product>> DobaviArtiklePorudzbine(int idPorudzbine);
        Task<List<Product>> DobaviArtiklePorudzbineProdavac(int idPorudzbine, int idProdavca);
    }
}
