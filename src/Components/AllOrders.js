import React from "react";
import { SvePorudzbine} from "../Services/Orders";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";

const AllOrders =()=>
{
    const [porudzbine, setPorudzbina] = useState([]);
    const navigate = useNavigate('');

    useEffect(() => {
        const get = async() => 
        {
          const response = await SvePorudzbine();
          console.log(response);
          console.log(response.data);

          setPorudzbina(response.data);
        }
        get();
      }, []);  
    return(
        <div>
    <table className="levo">
      <thead>
        <tr>
          <th>ID Porudžbine:</th>
          <th>Cena:</th>
          <th>Komentar:</th>
          <th>Adresa:</th>
          <th>Datum i vreme dostave:</th>
          <th>Status porudzbine:</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {porudzbine.map((porudzbina) => (
          <tr key={porudzbina.id}>
            <td>{porudzbina.id}</td>
            <td>{porudzbina.price}</td>
            <td>{porudzbina.comment}</td>
            <td>{porudzbina.address}</td>
            <td>{porudzbina.deliveryTime}</td>
            <td>
                {(() => {
                    switch (porudzbina.statusOrder) {
                    case 0:
                        return "OTKAZANA";
                    case 1:
                        return "PRIHVACENO,";
                    case 2:
                        return "ODBIJENO";
                    case 3:
                        return "U TOKU";
                    default:
                        return "";
                    }
                })()}
              </td>
            
          </tr>
        ))}
      </tbody>
    </table>
    <Link to="/dashboard"> Dash Board</Link>
    </div>
  );

};
export default AllOrders;