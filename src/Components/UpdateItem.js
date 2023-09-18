import { useState, useEffect } from "react";
import { AzurirajArtikal, ObrisiArtikal, SviArtikliProdavca } from "../Services/Items";
import jwtDecode from "jwt-decode";
import { Link } from "react-router-dom";
//import validator from 'validator';
const UpdateItem =()=>
{

    const [naziv, setNaziv] = useState('');
    const [cena, setCena] = useState(0);
    const [kolicina, setKolicina] = useState(0);
    const [opis, setOpis] = useState('');
    const [slika, setSlika] = useState(null);
    const[message, setMessage] = useState('');
    const [artikal, setArtikal] = useState([]);
    const [artikli, setArtikli] = useState([]);
    const [id, setID] = useState([]);

    const token = localStorage.getItem('jwtToken');
    const dekodiranToken = jwtDecode(token);
    const idd = dekodiranToken['UserID'];
    const azurirani = async () => {
      
        try {
          //const response = await SviArtikliProdavca(idd);
          //setArtikli(response.data);

        } catch (error) {
          console.error('Greška prilikom dobavljanja artikala:', error);
        }
      };



      const handleObrisi = async (id) => {

        if(id < 0)
        {
          setMessage('ID mora biti pozitivan broj.');
        }
  
          try {
  
            await ObrisiArtikal(id);
            setID('');
            azurirani();
  
          } 
          catch (error) {
              console.log(error);
          }
        };  
  
  
        useEffect(() => {
          const getArtikli = async () => {
            try {
              const response = await SviArtikliProdavca(idd);
              setArtikli(response.data);
              console.log(response.data);
  
            } catch (error) {
              console.error('Greška prilikom dobavljanja artikala:', error);
            }
          }
          getArtikli();
        }, []);
      
  
  
      const handleAzurirajArtikal = async() => {
    
          const formData = new FormData();
          formData.append('Id', id);
          formData.append('Name', naziv);
          formData.append('Price', cena);
          formData.append('Quantity', kolicina);
          formData.append('Description', opis);
          formData.append('Picture', slika);
  
          if (id.trim() === 0 ||
          naziv.trim() === '' ||
          cena.trim() === 0 ||
          kolicina.trim() === 0 ||
          opis.trim() === '' ||
          slika === null) {
        
        setMessage('Sva polja su obavezna');
        return;
      }
  
    //   if (!validator.isAlpha(naziv)) {
    //     setMessage('Naziv može sadržavati samo slova.');
    //     return;
    //   }
        
      if(cena < 0)
      {
        setMessage('Cena mora biti pozitivan broj.');
      }
          
      if(id < 0)
      {
        setMessage('ID mora biti pozitivan broj.');
      }
        
    
      if(kolicina < 0)
      {
        setMessage('Kolicina mora biti pozitivan broj.');
      }
  
      try{
  
        await AzurirajArtikal(id, formData);
              setArtikal({
                  ...id, naziv, cena, kolicina, opis, slika 
              });
            setID('');
            setNaziv('');
            setCena('');
            setKolicina('');
            setOpis('');
            setSlika(null);
            azurirani();
  
            }
            catch(err)
            {
              console.log(err);
            }
      };
  
  

    return(
        <div >
        <h2>Dostupni artikli</h2>
            <table className="levo">
              <thead>
                <tr>
                  <th>Id artikla</th>
                  <th>Naziv</th>
                  <th>Cena</th>
                  <th>Količina</th>
                  <th>Opis</th>
                  <th>Slika</th>
                </tr>
              </thead>
              <tbody>
                {artikli.map((art) => (
                  <tr key={art.artikalID}>
                    <td>{art.id}</td>
                    <td>{art.name}</td>
                    <td>{art.price}</td>
                    <td>{art.quantity}</td>
                    <td>{art.description}</td>
                    <td>
                    <img  src={art.picture}
                    alt="Slika artikla"
                    width={20}
                    height={20}
                    ></img>
                         
                     </td>
                  </tr>
                ))}
              </tbody>
            </table>
       
  
  
          <h3>Ažuriranje artikla:</h3>
       
          <div className="kontejner">
          <form className="form">
          <div>
          <label>
              ID:
              <input
              type="number"
              value={id}
              onChange={(e) => setID(e.target.value)}
              />
          </label>
  
          </div>
          <div>
          <label>
              Naziv:
              <input
              type="text"
              value={naziv}
              onChange={(e) => setNaziv(e.target.value)}
              />
          </label>
  
          </div>
          <br />
          <div>
          <label>
              Cena:
              <input
              type="text"
              value={cena}
              onChange={(e) => setCena(e.target.value)}
              />
          </label>
  
          </div>
          <br />
          <div>
          <label>
              Kolicina:
              <input
              type="text"
              value={kolicina}
              onChange={(e) => setKolicina(e.target.value)}
              />
          </label>
          
          </div>
          <br />
          <div >
          <label>
              Opis:
              <input
              type="text"
              value={opis}
              onChange={(e) => setOpis(e.target.value)}
              />
          </label>
          </div>
  
          <br />
          <div>
          <label>
              Odaberite sliku:
              <input
              type="file"
              accept="image/*"
              onChange={(e) => setSlika(e.target.files[0])}
              />
          </label>
          </div>
          
          <button className='custom-button' type="button" onClick={handleAzurirajArtikal}>
              Ažuriraj artikal
          </button>
          </form>
          <div>
          <p>  {message && (
          <div>
            <p style={{ color: 'red' }}> Greska: {message}</p>
          </div>
            )}</p>
  
          </div>
  
  <div>

 
      <h2>Obriši artikal</h2>
      <label>Id:</label>
        <input
          type="number"
          name="id"
          value={id}
          onChange={(e) => setID(e.target.value)}
        />
        <br></br>
        <p></p>
        <button className="custom-button" type="button" onClick={() => handleObrisi(id)}>
          Obriši
        </button>
        <div>
        <Link to="/dashboard">Dash board</Link>
        </div>
    </div>
  </div>
  </div>
    );

};
export default UpdateItem;