
import React, { useState, useEffect } from "react";
import {  NovaPorudzbina, SvePorudzbine } from "../Services/Orders";

import { SviArtikli } from "../Services/Items";
//import {  PrethodnePorudzbineKupca } from "../servisi/PorudzbinaServis";
import { Link } from "react-router-dom"
const NewOrder =()=>
{


    const [address, setAdresa] = useState('');
    const [comment, setKomentar] = useState('');
    const [artikli, setArtikli] = useState([]);
    const [items, setItems] = useState([]);

    const [odabraniArtikal, setOdabraniArtikal] = useState(null);
    const [quantity, setKolicina] = useState(0);
    const [stavke, setStavke] = useState([]);
    const[error, setError] = useState('');
    const osveziArtikle = async () => {
        try {
          const response = await SviArtikli();
          setArtikli(response.data);
        } catch (error) {
          console.error('Greška prilikom osvežavanja artikala:', error);
        }
      };
      useEffect(() => {
        const getArtikli = async () => {
          try {
            const response = await SviArtikli();
            setArtikli(response.data); //da ali ja ne zelim ovo da saljem kao podatak za items vec samo taj artikal izabrani
          } catch (error) {
            console.error('Greška prilikom dobavljanja artikala:', error);
          }
        }
        getArtikli();
      }, []);

      const handleArtikalChange = (artikalID) => {
        setOdabraniArtikal(artikalID);
        setKolicina(0);
      };
  
      const handleKolicinaChange = (e) => {
        setKolicina(Number(e.target.value));
      };
  
      const handleDodajStavku = () => {
        if (odabraniArtikal && quantity > 0) {
          const artikal = artikli.find((artikal) => artikal.id=== odabraniArtikal);
          const novaStavka = {
            artikalStavkaID: artikal.id,
            quantity: quantity
          };
          setItems(prevStavke => [...prevStavke, novaStavka]);
          setOdabraniArtikal(null);
          setKolicina(0);
        }
      };
  
    const handleSubmit = async (e) => {
      e.preventDefault();
  
      if (address.trim() === '' ||
        quantity === 0 ||
        comment.trim() === '' ) {
        setError('Sva polja su obavezna.');
      return;
      }
  
      try {
        const reqData = {
          address,
          items,
          comment
        };
  
      const idPorudzbine = await NovaPorudzbina(reqData);
      console.log(idPorudzbine);
  
      setAdresa('');
      setStavke([]);
      setKomentar('');
  
      osveziArtikle();
      await SvePorudzbine();
      //await PrethodnePorudzbineKupca();
  
     // const por = await DobaviPorudzbinuPoID(idPorudzbine);
   
        
  
      } catch (error) {
        console.error(error);
      }
    };
  
    return(
        <div>
      
      <form onSubmit={handleSubmit}>
        <div className="first">
          
        </div>

        <div>
          <h4>Dostupni artikli</h4>
          <table className="levo">
            <thead>
              <tr>
                <th>ArtikalID</th>
                <th>Naziv</th>
                <th>Cena</th>
                <th>Količina</th>
                <th>Opis</th>
                <th>Slika</th>
                <th>Odaberi</th>
              </tr>
            </thead>
            <tbody>
              {artikli.map((artikal) => (
                <tr key={artikal.id}>
                  <td>{artikal.id}</td>
                  <td>{artikal.name}</td>
                  <td>{artikal.price}</td>
                  <td>{artikal.quantity}</td>
                  <td>{artikal.description}</td>
                  
                  <td>
                  <img  className='img-art' src={artikal.picture}></img>
                  </td>
                  <td>
                    <input
                      type="radio"
                      name="odabraniArtikal"
                      value={artikal.id}
                      checked={odabraniArtikal === artikal.id}
                      onChange={() => handleArtikalChange(artikal.id)}
                    />
                  </td>
                </tr>
              ))}
        
            </tbody>
          </table>
        </div>

        <div className="form">
        <label htmlFor="address">Adresa:</label>
          <input
            type="text"
            id="address"
            name="address"
            value={address}
            onChange={(e) => setAdresa(e.target.value)}
          />
          <label htmlFor="quantity">Količina:</label>
          <input
            type="number"
            id="quantity"
            name="quantity"
            value={quantity}
            min="1"
            onChange={handleKolicinaChange}
          />
          <button type="button" className="custom-button" onClick={handleDodajStavku}>Dodaj</button>
        </div>

        <div className="form">
          <label htmlFor="comment">Komentar:</label>
          <textarea
            id="comment"
            name="comment"
            value={comment}
            onChange={(e) => setKomentar(e.target.value)}
          />
        </div>

        <button className="custom-button" type="submit">Poruči</button>
      </form>
   
<br/><br/>
<label  htmlFor="/dashboard"> <Link to="/dashboard">Dash Board</Link> </label>
    </div>
    
    );

};
export default NewOrder;