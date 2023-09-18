import {fetchUserProfile} from '../Services/UpdateUser.js'
import jwt_decode from "jwt-decode";
import  { useEffect,useState } from 'react';
import { Link, Routes, useHref } from 'react-router-dom';


const Profile2 = () => {
    const [userProfile, setUserProfile] = useState({});

    const token = localStorage.getItem('jwtToken');
    const decodedToken = jwt_decode(token);
    
          // Izvlačenje podataka iz dekodiranog tokena
    const userId = decodedToken['UserID'];
    useEffect(() => {
      const fetchData = async () => {
        try {
          const u = await fetchUserProfile(userId);
          setUserProfile(u);
          console.log(u.name);
          //console.log(userProfile);
        } catch (error) {
          console.error('Error fetching user profile:', error);
        }
      };
    
      fetchData();
    }, []);
console.log(userProfile);
    return (

        <div>
          <img  className='img' src={userProfile.imagePath}></img>
            <table className='levo' >
        <tbody>
          
          <tr>
            <th>Ime</th>
            <td>{userProfile.name}</td>
          </tr>
          <tr>
            <th>Prezime</th>
            <td>{userProfile.lastName}</td>
          </tr>
          <tr>
            <th>Korisničko ime</th>
            <td>{userProfile.userName}</td>
          </tr>
          <tr>
            <th>Email</th>
            <td>{userProfile.email}</td>
          </tr>
          <tr>
            <th>Adresa</th>
            <td>{userProfile.address}</td>
          </tr>
          <tr>
            <th>Datum rođenja</th>
            <td>{userProfile.dateOfBirth}</td>
          </tr>
          
         
            
          
          <tr >
            <td colSpan={2}><Link to="/updateProfile">
          <button  className='custom-button'> Ažuriraj svoje podatke</button>
          </Link></td>
            
          
          </tr>
          </tbody>
          </table>
          
        </div>
        
    );
}

export default Profile2;