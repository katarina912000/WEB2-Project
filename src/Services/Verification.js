import { ax } from '../Axios.js';
import jwt_decode from "jwt-decode";

export const AllSellers = async() => {
    const token = localStorage.getItem('jwtToken');
    //const decodedToken = jwt_decode(token);

    // const userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    // console.log("allsellers trenutno je ulogovan: " +token);
    ax.defaults.headers.common["Authorization"] = `Bearer ${token}`;
//trebalo bi da vrati listu
    return await ax.get(`/verificationSellers`);
};

export const VerifikacijaProdavca = async(email,verifikacija) => {


   try {
    console.log(`/send/${email}/${verifikacija}`);
    if(verifikacija==='acc'){
        const response = await ax.post(`/send/${email}/${verifikacija}`,
           
        {
            headers:
            {"Content-Type": "application/json"},
        });   
        const token = response.data; 
   
    ax.defaults.headers.common['Authorization'] = `Bearer ${token}`;

    return response.data;
    }else{
        const response = await ax.post(`/send/${email}/${verifikacija}`,
           
        {
            headers:
            {"Content-Type": "application/json"},
        });
        const token = response.data; 
   
    ax.defaults.headers.common['Authorization'] = `Bearer ${token}`;

    return response.data;

    }
   
    
    
} catch (err) {
    alert("Greska prilikom senda" + err);
    return null;
  }
};