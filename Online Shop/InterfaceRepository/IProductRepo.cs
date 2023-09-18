using Online_Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Shop.InterfaceRepository
{
    public interface IProductRepo
    {
        Task DodajArtikal(Product artikal);
        Task<List<Product>> DobaviSveArtikle();// ovo ce trebati za update i delete
        Task<Product> DobaviArtikalpoID(int id);

        Task<Product> AzurirajArtikal(Product artikal);

        Task<Product> ObrisiArtikal(int id);

        Task<bool> DaLiJeDostupanArtikal(Product artikalDTO);

        Task<List<Product>> DobaviSveArtikleProdavca(int idProdavca);
    }
}
