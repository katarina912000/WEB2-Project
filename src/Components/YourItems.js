const YourItems =()=>
{
    return(
        <div>
        <h2>Moje porudzbine</h2>
      <table className="levo">
        <thead>
          <tr>
            <th>ID Porudžbine:</th>
            <th>Cena:</th>
            <th>Komentar:</th>
            <th>Adresa:</th>
            <th>Datum i vreme dostave:</th>
            <th>Status porudzbine:</th>
          </tr>
        </thead>
        <tbody>
          {/* {porudzbine.map((porudzbina) => (
            <tr key={porudzbina.porudzbinaID}>
              <td>{porudzbina.porudzbinaID}</td>
              <td>{porudzbina.cijena}</td>
              <td>{porudzbina.komentar}</td>
              <td>{porudzbina.adresa}</td>
              <td>{porudzbina.datumVrijemeDostave}</td>
              <td>
                  {(() => {
                      switch (porudzbina.statusPorudzbine) {
                      case 0:
                          return "U_TOKU";
                      case 1:
                          return "ODBIJENO,";
                      case 2:
                          return "PRIHVACENO";
                      case 3:
                              return "OTKAZANA";   
                      default:
                          return "";
                      }
                  })()}
                </td> */}
                {/* <td>
                    {porudzbina.slika && ( 
                        <img src={`data:image/png;base64,${porudzbina.slika}`} 
                         alt="Slika korisnika"
                         width={80}
                        height={80}
                    />)}
               </td>   */}
                {/* <td>
                  <button className="btn" onClick={() => prikaziSveArtikleProdavac(porudzbina.porudzbinaID)}>
                    OPSIRNIJE
                  </button>
                </td>
            </tr>
          ))} */}
        </tbody>
      </table>
      {/* <label className='nazad' htmlFor="/dashBoard"> <Link to="/dashBoard">Dash Board</Link> </label> */}
      </div>
    );
};
export default YourItems;