import react, { useState}  from 'react';
import { useHistory, useNavigate } from 'react-router-dom';
import Select from 'react-select';
import { RegistrationService } from '../Services/RegistrationService';

const  Registration = () => 
{
    const [name, setName] = useState('');
    const [lastName, setLastName] = useState('');
    const [userName, setUserName] = useState('');
    const [email, setEmail] = useState('');   
    const [password, setPassword] = useState('');
    const [password2, setPassword2] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');
    const [address, setAddress] = useState('');
    const [picture, setPicture]=useState(null);
    const [role, setRole] = useState(null);

    const navigate=useNavigate();
    const [showUploadMessage, setShowUploadMessage] = useState(false);

    const enumMap = {
      CUSTOMER: 2,
      SELLER: 1,
    };
    const handleFileChange = (event) => {
      const file = event.target.files[0];
      setPicture(file);
    };
    const handleSubmit = async(event) =>
    {
      event.preventDefault();

      const enumNumber=enumMap[role];
      const formData=new FormData();
      
      if(!picture)
      { 
        showUploadMessage=true;
        setShowUploadMessage('Odaberite sliku.');
        return;
      }

      formData.append('Name',name);
      formData.append('LastName',lastName);
      formData.append('UserName',userName);
      formData.append('Email',email);
      formData.append('Password',password);
      formData.append('Password2',password2);
      formData.append('DateOfBirth',dateOfBirth);
      formData.append('Address',address);
      formData.append('ImagePath',picture);
      formData.append('Role',enumNumber);

      const formDataArray = Array.from(formData.entries());
      console.log('FormData ključevi i vrednosti:', formDataArray);
      try{

        const response= await RegistrationService(formData);
        setName('');
        setLastName('');
        setUserName('');
        setEmail('');
        setPassword('');
        setPassword2('');      
        setDateOfBirth('');
        setAddress('');
        setPicture('');
        setRole('');

        console.log(JSON.stringify(formData));
        navigate('/dashboard');
      }catch(error){
        if (error.response && error.response.data && error.response.data.errors) {
          const errors = error.response.data.errors;
         
          
        } else {
          console.log(error);
        }
      }
  
    };
  
    const options = [
        { value: 'SELLER', label: 'SELLER' },
        { value: 'CUSTOMER', label: 'CUSTOMER' },
      ];
    
      const handleComboBoxChange = (role) => {
        setRole(role.value);
      };
  
  return (
    <div className="first">
    
    <form onSubmit={handleSubmit}>
    <div className='form'>
          <label htmlFor="name">Ime: </label>
          <input
            type="text"
            id="name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
     </div>      
     <div className='form'>
          <label htmlFor="lastName">Prezime: </label>
          <input 
            type="text"
            id="lastName"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
            required
          />
    </div>

    <div className='form'>
          <label htmlFor="userName">Korisnicko ime: </label>
          <input
            type="text"
            id="userName"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
            required
          />
    </div>

    <div className='form'>
          <label htmlFor="email">E-mail: </label>
          <input 
            type="email"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
    </div>
    <div className='form'>
          <label htmlFor="password">Lozinka: </label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
    </div>
    <div className='form'>
          <label htmlFor="password2">Potvrdi lozinku: </label>
          <input
            type="password"
            id="password2"
            value={password2}
            onChange={(e) => setPassword2(e.target.value)}
            required
          />
    </div>
    
    <div className='form'>
          <label htmlFor="dateOfBirth">Datum rođenja: </label>
          <input 
            type="date"
            id="dateOfBirth"
            value={dateOfBirth}
            onChange={(e) => setDateOfBirth(e.target.value)}
            required
          />
    </div>

    <div className='form'>
          <label htmlFor="address">Adresa: </label>
          <input 
            type="text"
            id="address"
            value={address}
            onChange={(e) => setAddress(e.target.value)}
            required
          />
    </div>
    <div className='form'>
        <label htmlFor="comboBox">Uloga: </label>
        <Select
          id="comboBox"
          value={role}
          onChange={handleComboBoxChange}
          options={options}
          
        />
    </div>
    <div>
      <label>Set picture:</label>
      <label>Set picture:</label>
        <input type="file" accept="image/*" onChange={handleFileChange} />
      {picture && (
        <p>Izabrani fajl: {picture.name}</p>
      )}
    </div>
    
     
    <button className='btn' type="submit"> Registruj se</button>
    <br/>
    </form>
   
    </div>
  );     
};
export default Registration;