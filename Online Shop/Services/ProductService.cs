using AutoMapper;
using Microsoft.AspNetCore.Http;
using Online_Shop.DTO;
using Online_Shop.InterfaceRepository;
using Online_Shop.Interfaces;
using Online_Shop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Online_Shop.Services
{
    public class ProductService : IProduct
    {
        private readonly IMapper maper;
        private readonly IProductRepo artikalRepozitorijum;
        private readonly IHttpContextAccessor httpContextAccessor;
        private string filePath;
        //
        public ProductService(IMapper map, IProductRepo artikalRepozitorijum,IHttpContextAccessor httpContextAccessor)
        {
            this.artikalRepozitorijum = artikalRepozitorijum;
            maper = map;
            this.httpContextAccessor = httpContextAccessor;
        }
        //
        public async Task<ProductDTO> AzurirajArtikal(int id, AddProductDTO artikalDTO)
        {
            var idCLaim = httpContextAccessor.HttpContext.User.FindFirst("UserID");


            if (idCLaim == null)
            {
                throw new Exception("Korisnik ne postoji u klejmu");
            }

            int idd;

            if (!int.TryParse(idCLaim.Value, out idd))
            {
                throw new Exception("Id nije konvertovan u broj");
            }

            var artikal = await artikalRepozitorijum.DobaviArtikalpoID(id);

            maper.Map(artikalDTO, artikal);

            if (artikal != null)
            {
                artikal.Price = artikalDTO.Price;
                artikal.Quantity = artikalDTO.Quantity;
                artikal.Name = artikalDTO.Name;
                artikal.Description = artikalDTO.Description;
                artikal.Picture= await SaveImage(artikalDTO.Picture);
            }
            else
            {
                throw new Exception(string.Format("Artikal ne postoji"));
            }


            if (artikalDTO.Name != artikalDTO.Name)
            {
                throw new Exception("Artikal sa istim nazivom postoji!");
            }

            if (artikalDTO.Price < 0)
            {
                throw new Exception("Cena artikla mora biti veca od 0!");
            }
            if (artikalDTO.Quantity < 0)
            {
                throw new Exception("Kolicina artikla mora biti veca od 0!");
            }


            var art = await artikalRepozitorijum.AzurirajArtikal(artikal);
            art.KorisnikID = idd;
            return maper.Map<ProductDTO>(art);
        }
        //
        public async Task<string> SaveImage(IFormFile image)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            filePath = Path.Combine("C:\\Users\\dabet\\Desktop\\FAKS\\WEB2\\git2vscode\\WEB2-Project\\uploads", uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            byte[] imageBytes = File.ReadAllBytes(filePath);
            string base64Image = Convert.ToBase64String(imageBytes);
            //dodato da se u bazu sacuva url a ne stvarna fizicka adresa
            var imageUrl = "https://localhost:44312/uploads/" + uniqueFileName; // URL za sliku
            string img64 = "data:image/jpeg;base64," + base64Image;

            return img64; // Vratite ime datoteke kako biste ga mogli spremiti u bazu
        }
        //
        public async Task<bool> DaLiJeDostupanArtikal(ProductDTO artikalDTO)
        {
            var artikal = maper.Map<Product>(artikalDTO);
            return await artikalRepozitorijum.DaLiJeDostupanArtikal(artikal);
        }
        //
        public async Task<ProductDTO> DobaviArtikalpoID(int id)
        {
            var artikal = await artikalRepozitorijum.DobaviArtikalpoID(id);

            return maper.Map<ProductDTO>(artikal);
        }
        //
        public async Task<List<ProductDTO>> DobaviSveArtikle()
        {
            var artikli = await artikalRepozitorijum.DobaviSveArtikle();
            return maper.Map<List<ProductDTO>>(artikli);
        }
        //
        public async Task<List<ProductDTO>> DobaviSveArtikleProdavca(int idProdavca)
        {
            var artikliProdavca = await artikalRepozitorijum.DobaviSveArtikleProdavca(idProdavca);

            if (artikliProdavca == null)
            {
                throw new Exception("Prodavac nema artikle");
            }

            return maper.Map<List<ProductDTO>>(artikliProdavca);
        }
        //
        public async Task DodajArtikal(AddProductDTO artikalDTO)
        {
            var idCLaim = httpContextAccessor.HttpContext.User.FindFirst("UserID");


            if (idCLaim == null)
            {
                throw new Exception("Korisnik ne postoji u klejmu");
            }
            int id;

            if (!int.TryParse(idCLaim.Value, out id))
            {
                throw new Exception("Id nije konvertovan u broj");
            }


            if (string.IsNullOrEmpty(artikalDTO.Name))
            {
                throw new Exception("Naziv artikla je obavezan");
            }


            if (artikalDTO.Price <= 0)
            {
                throw new Exception("Cijena artikla mora biti veća od 0");
            }


            var artikal = maper.Map<Product>(artikalDTO);
           
            artikal.Picture = await SaveImage(artikalDTO.Picture);

            artikal.KorisnikID = id;
            await artikalRepozitorijum.DodajArtikal(artikal);
        }
        //
        public async Task<ProductDTO> ObrisiArtikal(int id)
        {
            var artikal = await artikalRepozitorijum.ObrisiArtikal(id);
            return maper.Map<ProductDTO>(artikal);
        }
    }
}
