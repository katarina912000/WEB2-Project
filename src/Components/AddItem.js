
import React, { useState } from "react";
import { AzurirajArtikal, NoviArtikal, ObrisiArtikal } from "../Services/Items";
//import './tabele.css'
import '../App.css'
import { Link } from 'react-router-dom';
//import validator from 'validator'; 
const AddItem =()=>
{
    const [naziv, setNaziv] = useState('');
    const [cena, setCena] = useState(0);
    const [kolicina, setKolicina] = useState(0);
    const [opis, setOpis] = useState('');
    const [slika, setSlika] = useState(null);
    const[message, setMessage] = useState('');

    const handleSubmit = async(e) => {
        e.preventDefault();
  
  
        if (naziv.trim() === '' ||
        cena ===0||
        kolicina === 0||
        opis.trim() === '' ||
        slika === null) {
      
        setMessage('Sva polja su obavezna.');
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
  
      if(kolicina < 0)
      {
         setMessage('Kolicina mora biti pozitivan broj.');
      }
  
  
        const formData = new FormData();
        formData.append('Name', naziv);
        formData.append('Price', cena);
        formData.append('Quantity', kolicina);
        formData.append('Description', opis);
        formData.append('Picture', slika);
         
        
      try{
        //console.log(formData);
       const resp= await NoviArtikal(formData);
       console.log(resp);
          setNaziv('');
          setCena(0);
          setKolicina(0);
          setOpis('');
          setSlika(null);
      }
     catch(error)
     {
      console.error(error);
     }
    };

    return(
        <div className='first'>
      <h2>Dodaj artikal:</h2>
      <form onSubmit={handleSubmit}>
      <label htmlFor="naziv">Naziv:</label>
      <input
        type="text"
        id="naziv"
        name="naziv"
        value={naziv}
        onChange={(e) => setNaziv(e.target.value)}
      />
<br></br>
      <label htmlFor="cena">Cena:</label>
      <input
        type="number"
        id="cena"
        name="Cena"
        value={cena}
        onChange={(e) => setCena(e.target.value)}
      />
<br></br>
     <label htmlFor="kolicina">Kolicina:</label>
      <input
        type="number"
        id="kolicina"
        name="kolicina"
        value={kolicina}
        onChange={(e) => setKolicina(e.target.value)}
      />
<br></br>
      <label htmlFor="opis">Opis:</label>
      <input
        type="text"
        id="opis"
        name="opis"
        value={opis}
        onChange={(e) => setOpis(e.target.value)}
      />
     
      <div className='form'>
        <label> Odaberite sliku: </label>
        <input
          type="file"
          accept="image/*" 
          onChange={(e) => setSlika(e.target.files[0])}
      />
      </div> 
<br></br>
<br></br>

      <button className="custom-button" type="submit">Dodaj artikal</button>
    </form> <br/>
    <br/>


    <label className='nazad' htmlFor="/dashboard"> <Link to="/dashboard">Dash Board</Link> </label>
     <p>{message && (
        <div>
          <p style={{ color: 'red' }}> Greska: {message}</p>
        </div>
      )}</p>
</div>
    );

};
export default AddItem;