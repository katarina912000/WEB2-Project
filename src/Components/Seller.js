import { useNavigate } from 'react-router-dom';
import { Link, Routes, useHref } from 'react-router-dom';
import jwt_decode from "jwt-decode";
import React, { useState,useEffect } from 'react';
import {fetchUserProfile} from '../Services/UpdateUser.js'

const Seller = () => 
{
    const navigate = useNavigate();
    const token = localStorage.getItem('jwtToken');
    console.log(token);
    const decodedToken = jwt_decode(token);
    const userStatus = decodedToken['StatusApproval'];
    const userId = decodedToken['UserID'];
    console.log(userId);
    console.log(userStatus);

    //ipak moram dobaviti preko id-a
    const [userProfile, setUserProfile] = useState({});
    useEffect(() => {
        const fetchData = async () => {
          try {
            const u = await fetchUserProfile(userId);
            setUserProfile(u);
            console.log(u);
            console.log(u.name);
            console.log(u.verified);

            //console.log(userProfile);
          } catch (error) {
            console.error('Error fetching user profile:', error);
          }
        };
      
        fetchData();
      }, []);

      let content;
      if (userProfile.verified) {
        if (userProfile.statusApproval === 0 && userProfile.verified==true) {
          console.log("prihvacena");
          content = (
            <div className="first">
              <p>zdravo prodavce!</p>
              {/* <Link to="/profile2">
                <button className="custom-button">Profil korisnika</button>
              </Link> */}
            </div>
          );
        } else if (userProfile.statusApproval === 1 && userProfile.verified==true) {
          console.log("odbijena");
          content = (
            <div className="first">
              <p>Vaš pokušaj registracije je odbijen nazalost</p>
            </div>
          );
        } 
      } else {
        // Dodajte odgovarajući JSX za situaciju kada nije verifikovan
        console.log("u procesu je");
        content = (
          <div className="first">
            <p>Vaša registracija je u procesu obrade.</p>
          </div>
        );
      }
      
      return content;
 // ovde dobaviti tog trenutno preko id-ja i pitati da li mu pise verified true, approvedi tek onda ovo prikazati a ako ne ispisati odredjenu poruku
    
    
};
export default Seller;