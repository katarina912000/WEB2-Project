import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { ax } from '../Axios.js';


//getuserbyid
export const fetchUserProfile = async (id) => {
    try {
       // const token = localStorage.getItem("jwtToken");

      const response = await ax.get('/'+id); //ovde treba id da ide
      //response vraca nam usera
      return response.data;
     // console.log(response.data);
    } catch (error) {
      console.error('Error fetching user profile:', error);
    }
  };
//put
//
  export const updateService = async (id,formData) => {
    try {
      // Ovde pozovite API za ažuriranje korisničkih podataka
      const resp=await ax.put('/'+id, formData,
      {headers: 
        {
          "Content-Type":"multipart/form-data",
        },
      }); // Promenite stazu prema vašem API-ju
      //console.log(resp);
      return resp;
      //setEditing(false);
    } catch (error) {
      console.error('Error updating user profile:', error);
    }
  };