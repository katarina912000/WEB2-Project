import { useState, useEffect } from "react";
import { AllSellers, VerifikacijaProdavca } from '../Services/Verification';
import jwt_decode from "jwt-decode";

const Admin = () => 
{

    const [sell, setProdavac] = useState([]);
   
    
    const token = localStorage.getItem('jwtToken');
    //const t=sessionStorage.getItem("token")
    
   const decodedToken = jwt_decode(token);

    const userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
   //console.log("allsellers trenutno je ulogovan: " +userRole);
    useEffect(() => {
      const get = async() => 
      {
        const response = await AllSellers();
        console.log(response.data);
        setProdavac(response.data);
        console.log(sell);

      }
      get();
    }, []);
    //console.log(sell.name);//ovo je undefined
    const handleAccept = (sellermail) => {
        try{
            console.log(sellermail);

            const ver=  VerifikacijaProdavca(sellermail,'acc');
            console.log(ver);

        }catch(error)
        {
         if (error.response && error.response.data && error.response.data.errors) {
           const errors = error.response.data.errors;
          
           
         } else {
           console.log(error);
         }
         }
      };
    
      // Metoda za odbijanje sellermail
      const handleReject = (sellermail) => {
        try{
            console.log(sellermail);
            const ver=  VerifikacijaProdavca(sellermail,'rej');
            console.log(ver);


        }catch(error)
        {
         if (error.response && error.response.data && error.response.data.errors) {
           const errors = error.response.data.errors;          
           
         } else {
           console.log(error);
         }
         }
       
      };
    return(
        <div className="first">
        <p>zdravo admine!</p>
        <table className="levo">
            <thead>
            <tr>
                <th colSpan={10}>Prodavci:</th>
            </tr>
            <tr>
           
              <th>Ime:</th>
              <th>Prezime:</th>
              <th>Korisnicko ime:</th>
              <th>Email:</th>
              <th>Verifikovan:</th>
              <th>Status verifikacije:</th>
              <th>Datum rodjenja:</th>
              <th>Adresa:</th>
              <th>Slika:</th>
              <th> </th>
            </tr>
            </thead>
            <tbody>
        {sell.map((seller, index) => (
          <tr key={index}>

            <td>{seller.name}</td>
            <td>{seller.lastName}</td>
            <td>{seller.userName}</td>
            <td>{seller.email}</td>
            <td>{seller.verified ? 'Da' : 'Ne'}</td> 
            {seller.verified ?  <td>
                {seller.statusApproval ? 'Odbijeno':'Prihvaćeno'}
                </td> : <td>
                Čeka na verifikaciju
                </td>
           }
            
            
            <td>{seller.dateOfBirth}</td>
            <td>{seller.address}</td>
            <td><img className ="adminSlika" src={seller.imagePath}></img></td>
            <td>
  {seller.verified ? (
    ''
  ) : (
    <>
      <button className="custom-button" key="accept" onClick={() => handleAccept(seller.email)}>
        Accept
      </button>
      <button className="custom-button" key="reject" onClick={() => handleReject(seller.email)}>
        Reject
      </button>
    </>
  )}
</td>
          </tr>
        ))}
      </tbody>

            
           
        </table>
        </div>
    );
};
export default Admin;