import {fetchUserProfile,updateService} from '../Services/UpdateUser.js'
import jwt_decode from "jwt-decode";
import  { useEffect,useState } from 'react';
import { Link, Routes, useHref } from 'react-router-dom';

const UpdateProfile =()=>
{
 
const [show, setShow] = useState(false);
const handleToggleDateChange = () => {
        setShow(!show);
      };
const [show1, setShow1] = useState(false);
const handleTogglePassChange = () => {
        setShow1(!show1);
      };
const [show2, setShow2] = useState(false);
const handleTogglePhotoChange = () => {
       setShow2(!show2);
     };
const handleInputChange = (e) => {
        //const { name, value } = e.target;
        const field = e.target.getAttribute('data-field');
        const value = e.target.value;
        if (value.trim() === " ") {
          show(true);
                  
          return;
        }
        setUserProfile((prevUser) => ({
          ...prevUser,
          [field]: value,
        }));
  
        show(false);
      };
const [userProfile, setUserProfile] = useState({});
//napraviti jos jedan profil kako bi mogla uporediti da li je bilo razlike i uzeti novu cvrednosst a da nije prazna
//razmestiti userprofile tako da ima posebno ime,prz,email i to i onda tako ih sacuvati

const [name, setName] = useState('');
const [lastName, setLastName] = useState('');
const [userName, setUserName] = useState('');
const [email, setEmail] = useState('');   
const [address, setAddress] = useState(''); 
const [dateOfBirth, setDateOfBirth] = useState('');
const [picture, setPicture]=useState(null);
const [password, setPassword] = useState('');
const [id, setId] = useState('');





const token = localStorage.getItem('jwtToken');
const decodedToken = jwt_decode(token);

      // Izvlačenje podataka iz dekodiranog tokena
const userId = decodedToken['UserID'];
useEffect(() => {
  const fetchData = async () => {
    try {
      const u = await fetchUserProfile(userId);
      setId(userId);
      setUserProfile(u);
      setEmail(u.email);
      setName(u.name);
      setLastName(u.lastName);
      setUserName(u.userName);
      setAddress(u.address);
      setDateOfBirth(u.dateOfBirth);
      setPicture(u.imagePath);
      setPassword(u.password);
    } catch (error) {
      console.error('Error fetching user profile:', error);
    }
  };

  fetchData();
}, []);
console.log(userProfile);//tu su sve info o tom objektu


const handleSubmit = async(e) =>
    {
      e.preventDefault();
      //treba ovde dodati logiku za prvih 5 provera da li se desila promena u polju!
      
      console.log(userProfile);
      const formData = new FormData(e.target);
      console.log("udario je submit");
      //ovde treba da ide logika ako je prazan string da uzme staru vrednost
      if(formData.get("name").length!==0){
        setName(formData.get("name"));
        console.log(formData.get("name"));

      }else{
        console.log(name);
      }
      if(formData.get("lastName").length!==0){
        setLastName(formData.get("lastName"));
        console.log(formData.get("lastName")); // po name a ne po id

      }
      
      else{
        console.log(lastName);
      }
      if(formData.get("email").length!==0){
        setEmail(formData.get("email"));
        console.log(formData.get("email")); // po name a ne po id

      }
      else{
        console.log(email);
      }
      if(formData.get("userName").length!==0){
        setUserName(formData.get("userName"));
        console.log(formData.get("userName")); // po name a ne po id

      }
      else{
        console.log(userName);
      }
      if(formData.get("address").length!==0){
        setAddress(formData.get("address"));
        console.log(formData.get("address")); // po name a ne po id

      }else{
        console.log(address);
      }
      if(formData.get("pass")!==null){
        setPassword(formData.get("pass"));
        console.log(formData.get("pass")); // po name a ne po id

      }
      else{
        console.log(password);
      }
     if(formData.get("DateOfBirth")!==null){
      setDateOfBirth(formData.get("DateOfBirth"));
      console.log(formData.get("DateOfBirth"));
     }else{
      console.log(dateOfBirth);
     }
     if(formData.get("photo")!==null){
      setPicture(formData.get("photo"));
      console.log(formData.get("photo"));
     }else{
      console.log(picture);
     }
    //sad ovde ide poziv i kreiranje objekta koji saljemo kao formdata
    const fd= new FormData();
    fd.append('Name',name);
    fd.append('LastName',lastName);
    fd.append('UserName',userName);
    fd.append('Email',email);
    fd.append('Password',password);   
    fd.append('DateOfBirth',dateOfBirth);
    fd.append('Address',address);
    fd.append('ImagePath',picture);

    const formDataArray = Array.from(fd.entries());
      console.log('FormData ključevi i vrednosti:', formDataArray);

      //zahtevciccc

      try{
        const response=await updateService(id,fd);

      }catch(error)
      {
        if (error.response && error.response.data && error.response.data.errors) {
        const errors = error.response.data.errors;
       
        
      } else {
        console.log(error);
      }}

    };
return(
    <div>
      <img src={userProfile.imagePath} className='img'>
      </img>
      <form onSubmit={handleSubmit}> 

      
    <table className='levo' id='mojaTabela' >
<tbody>
  
  <tr>
    <th>Ime</th>
    <td>
        <input type='text' placeholder= {userProfile.name} id='name'   name='name'></input>
       </td>
  </tr>
  <tr>
    <th>Prezime</th>
    <td> 
        <input type='text' placeholder= {userProfile.lastName} id='lastName' name='lastName'></input>
       </td>
  </tr>
  <tr>
    <th>Korisničko ime</th>
    <td><input type='text' placeholder= {userProfile.userName}id='userName'name='userName' ></input></td>
  </tr>
  <tr>
    <th>Email</th>
    <td><input type='text' placeholder= {userProfile.email} id='email'name='email'></input></td>
  </tr>
  <tr>
    <th>Adresa</th>
    <td><input type='text' placeholder= {userProfile.address} id='address' name='address'></input></td>
  </tr>
  <tr>
    <td colSpan={2}>
        <button  onClick={handleToggleDateChange} className='custom-button'>
        {show ? 'Odustani od promene datuma' : 'Promeni datum'}
           
        </button>
        {show && (
        <div>        
          <input type="date" name="DateOfBirth" id='DateOfBirth' /*onChange={handleInputChange}*/ />
          <button type="submit" >Sačuvaj</button>
          </div>
      )}
    </td>
  </tr>
  <tr>
    <td colSpan={2}>
        <button  onClick={handleTogglePassChange} className='custom-button'>
        {show1 ? 'Odustani od promene lozinke' : 'Promeni lozinku'}
            
        </button>
        {show1 && (
        <div>        
          <input type="password" name="pass" /*onChange={handleInputChange}*/ />
          <button type="submit" >Sačuvaj</button>
          </div>
      )}
    </td>
  </tr>
  <tr>
    <td colSpan={2}>
        <button  onClick={handleTogglePhotoChange} className='custom-button'>
        {show2 ? 'Odustani od promene fotografije' : 'Promeni foto'}
            
        </button>
        {show2 && (
        <div>        
          <input 
            type="file" 
            accept="image/*" name='photo'
          />
          <button type="submit" >Sačuvaj</button>
          </div>
      )}
    </td>
  </tr>

  
  <tr >
    <td ><Link to="/profile2">
  <button  className='custom-button'  > Back</button>
  </Link></td>
  <td>   <button className='custom-button' type='submit'>Update </button>
</td>
  </tr>
  </tbody>
  </table>
  </form>
  
</div>
);
};

export default UpdateProfile;