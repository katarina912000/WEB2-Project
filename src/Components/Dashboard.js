import React, { useState,useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

import jwt_decode from "jwt-decode";

const Dashboard = () => 
{
    const navigate=useNavigate();
    useEffect(() => {
        //console.log("stigao sam na dashboard stranicu!");
        // Preuzimanje tokena iz localStorage
        const token = localStorage.getItem('jwtToken');
        //console.log(token.toString());
        // if(token==null){
        //     console.log("token je null");
        // }
    if(token){
        
          // Dekodiranje JWT tokena
          const decodedToken = jwt_decode(token);
    
          // Izvlačenje podataka iz dekodiranog tokena
          const userId = decodedToken['UserID'];
          const userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    
          // Ovdje možete koristiti userId i userRole za dalje akcije
          console.log('User ID:', userId);
          console.log('User role:', userRole);
          if(userRole=="ADMIN"){
            //
            console.log(token);
            localStorage.setItem('jwtToken', token);
                        navigate('/admin');
        }
        if(userRole=="SELLER"){
            
            navigate('/seller');
         
        }
        if(userRole=="CUSTOMER"){
            navigate('/customer')
        }
    
    }
      }, []);
    

    
      
    
    
    
};
export default Dashboard;