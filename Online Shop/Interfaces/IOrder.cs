using Online_Shop.DTO;
using Online_Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Shop.Interfaces
{
    public interface IOrder
    {
        Task<List<Order>> DobaviSvePorudzbine();
        Task<Order> DobaviPorudzbinuPoID(int id);
        Task<int> DodajPoruzbinu(AddOrderDTO porudzbinaDTO);
        Task<OrderDTO> AzurirajPorudzbinu(int id, OrderDTO porudzbinaDTO);
        Task<OrderDTO> ObrisiPorudzbina(int id);
        Task<List<Order>> DobaviPrethodnePorudzbineZaKupca(int id);
        Task OtkaziPorudzbinu(int id);
        Task<List<Order>> DobaviSvePorudzbineZKupca(int kupacID);
        Task<List<Order>> NovePorudzineProdavca(int id);
        Task<List<Order>> MojePorudzbineProd(int id);
        Task<List<Product>> DobaviArtiklePorudzbine(int idPorudzbine);
        Task<List<Product>> DobaviArtiklePorudzbineProdavac(int idPorudzbine);
    }
}
