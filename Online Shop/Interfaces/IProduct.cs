using Online_Shop.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Shop.Interfaces
{
    public interface IProduct
    {
        Task DodajArtikal(AddProductDTO artikalDTO);
        Task<List<ProductDTO>> DobaviSveArtikle();
        
        Task<ProductDTO> DobaviArtikalpoID(int id);

        Task<ProductDTO> AzurirajArtikal(int id, AddProductDTO artikalDTO);

        Task<ProductDTO> ObrisiArtikal(int id);

        Task<bool> DaLiJeDostupanArtikal(ProductDTO artikalDTO);

        Task<List<ProductDTO>> DobaviSveArtikleProdavca(int idProdavca);


    }
}
