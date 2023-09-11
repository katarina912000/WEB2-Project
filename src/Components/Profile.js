import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useParams } from 'react-router-dom';
import { ax } from '../Axios.js';
import {fetchUserProfile,handleSaveClick} from '../Services/UpdateUser.js'
import jwt_decode from "jwt-decode";
import { all } from 'axios';

const Profile = () => {
  
  const [user, setUser] = useState({
    Name: '',
    LastName: '',
    UserName: '',
    Email: '',
    Password: '',
    Password2: '',
    DateOfBirth: '',
    Address: '',
    ImagePath: null,
  });
  const [show, setShow] = useState(
    
    
    
    
    false);
  const [show2, setShow2] = useState(false);
  const [show3, setShow3] = useState(false);

  const handleTogglePasswordChange = () => {
    setShow(!show);
  };
  const handleTogglePhotoChange = () => {
    setShow3(!show3);
  };
  const handleToggleDateChange = () => {
    setShow2(!show2);
  };
  // const SaveDate =  () =>{

  //   if()

  // };

  
  const [userProfile, setUserProfile] = useState({});
  const [isEmptyMessageVisible, setIsEmptyMessageVisible] = useState(false);

  //const [editing, setEditing] = useState(false);
    const navigate = useNavigate();
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
    const handleInputChange = (e) => {
      //const { name, value } = e.target;
      const field = e.target.getAttribute('data-field');
      const value = e.target.value;
      if (value.trim() === " ") {
        show(true);
        show2(true);
        show3(true);        
        return;
      }
      setUserProfile((prevUser) => ({
        ...prevUser,
        [field]: value,
      }));

      show(false);
    };
  
    console.log(userProfile);

    const  handleAzuriraj  =async()=>
    {
      //proveriti da li je ista od polja prazno
      
        const formData = new FormData();

        formData.append('name',userProfile.name);
        formData.append('lastName',userProfile.lastName);
        formData.append('userName',userProfile.userName);
        formData.append('email',userProfile.email);
        formData.append('password',userProfile.password);
        formData.append('password2',userProfile.password2);
        formData.append('dateOfBirth',userProfile.dateOfBirth);
        formData.append('address',userProfile.address);
        formData.append('imagePath',userProfile.picture);
        const formDataArray = Array.from(formData.entries());
        console.log('FormData ključevi i vrednosti:', formDataArray);
        try{
          await handleSaveClick(userId,formData)

        }catch(error){
          console.log(error);
        }
      
      
    }

  

  // 




  const [imageUrl, setImageUrl] = useState('');

  // Ovde treba da dohvatite URL slike sa servera i postavite ga u 'imageUrl' stanje
  useEffect(() => {
    // Dohvatanje URL slike sa servera, na primer:
    const serverImageUrl = '/uploads/r.jpg';
    setImageUrl(serverImageUrl);
  }, []);

  return (
    <div>

   <div>
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
          </tbody>
          </table>
         </div>
         
    <div className='first'>
      <p className='p'>Update user profile</p>
        <div >
          <label>Name:</label>
          <input type="text" name="Name" data-field="Name" value={userProfile.name}
           />
          
          {isEmptyMessageVisible && <p className="empty-field-message">Polje ne sme biti prazno.</p>}
        </div>
         <div>
          <label>Last Name:</label>
          <input type="text" name="LastName" data-field="LastName" value={userProfile.lastName} onChange={handleInputChange} onBlur={(e) => {
    if (e.target.value.trim() === "") {
      setIsEmptyMessageVisible(true);
    }}} />
          {isEmptyMessageVisible && <p className="empty-field-message">Polje ne sme biti prazno.</p>}

        </div>
        <div>
          <label>User Name:</label>
          <input type="text" name="UserName" data-field="UserName" value={userProfile.userName} onChange={handleInputChange} onBlur={(e) => {
    if (e.target.value.trim() === "") {
      setIsEmptyMessageVisible(true);
    }}} />
          {isEmptyMessageVisible && <p className="empty-field-message">Polje ne sme biti prazno.</p>}

        </div>
        <div>
          <label>Email:</label>
          <input type="email" name="Email" data-field="Email" value={userProfile.email} onChange={handleInputChange} onBlur={(e) => {
    if (e.target.value.trim() === "") {
      setIsEmptyMessageVisible(true);
    }}} />
          {isEmptyMessageVisible && <p className="empty-field-message">Polje ne sme biti prazno.</p>}

        </div>
        <div>
          <label>Address:</label>
          <input type="text" name="Address" data-field="Address" value={userProfile.address} onChange={handleInputChange}  onBlur={(e) => {
    if (e.target.value.trim() === "") {
      setIsEmptyMessageVisible(true);
    }}}/>
          {isEmptyMessageVisible && <p className="empty-field-message">Polje ne sme biti prazno.</p>}

        </div>
        
        <div>
        <button className='custom-button' onClick={handleToggleDateChange}>
        {show2 ? 'Odustani od promene datuma' : 'Promeni datum'}
      </button>
      {show2 && (
        <div>
          <label>Date of birth:</label>
          <input type="text" name="DateOfBirth" /*onChange={handleInputChange}*/ />
          {isEmptyMessageVisible && <p className="empty-field-message">Polje ne sme biti prazno.</p>}

          <button type="submit" >Sačuvaj</button>
          </div>
      )}
          
        </div>
        <div>
        <button className='custom-button' onClick={handleTogglePasswordChange}>
        {show ? 'Odustani od promene lozinke' : 'Promeni lozinku'}
      </button>
      {show && (
        <div>
          <h3>Promena lozinke</h3>
          <form>
          <div>
          <label>Password:</label>
          <input type="text" name="Password"  /*onChange={handleInputChange}*/ />
          {isEmptyMessageVisible && <p className="empty-field-message">Polje ne sme biti prazno.</p>}

        </div>
        <div>
          <label>Password confirm:</label>
          <input type="text" name="Password2"  /*onChange={handleInputChange}*//>
          {isEmptyMessageVisible && <p className="empty-field-message">Polje ne sme biti prazno.</p>}

        </div>
        <button type="submit">Sačuvaj</button>
          </form>
        </div>
      )}
        </div>

        <div className='form'>
          
            <div>
        <button className='custom-button' onClick={handleTogglePhotoChange}>
        {show3 ? 'Odustani od promene slike' : 'Promeni fotografiju'}
      </button>
      {show3 && (
        <div>
          <input 
            type="file" 
            accept="image/*"
            value={userProfile.ImagePath && <img src={userProfile.imagePath} alt="Profile" />}
            // onChange={(e) => setUser.ImagePath(e.target.files[0])}
          />
          <button type="submit">Sačuvaj</button>
        </div>
      )}
          
        </div>
        
        </div>
        {/* Dodajte ostala input polja za unos */}
        <button className='custom-button' type="submit" onClick={handleAzuriraj}>Update Profile</button> 
      
    </div>
    </div>
  );

};

export default Profile;
