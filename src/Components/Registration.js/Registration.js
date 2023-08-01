import react, { useState}  from 'react';
import Select from 'react-select';
import { RegistrationService } from '../../Services/RegistrationService';

const  Registration = () => 
{
    const [name, setName] = useState('');
    const [lastName, setLastName] = useState('');
    const [userName, setUserName] = useState('');
    const [email, setEmail] = useState('');   
    const [password, setPassword] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');
    const [address, setAddress] = useState('');
    const [picture, setPicture]=useState(null);
    const [role, setRole] = useState(null);
    const enumMap = {
      CUSTOMER: 2,
      SELLER: 1,
    };
    const handleFileChange = (event) => {
      const file = event.target.files[0];
      setPicture(file);
    };
    const handleFileUpload = () => {
      if (!picture) {
        alert('Please select photo');
        return;
      }
    };
    const handleSubmit = async(event) =>
    {
      event.preventDefault();

      
      const enumNumber=enumMap[role];
      const formData=new FormData();
      // Create an object containing the form data
      formData.append('Name',name);
      formData.append('LastName',lastName);
      formData.append('UserName',userName);
      formData.append('Email',email);
      formData.append('Password',password);
      formData.append('DateOfBirth',dateOfBirth);
      formData.append('Address',address);
      formData.append('Picture',picture);
      formData.append('Role',enumNumber);

      console.log(formData);

      try{

        const response= await RegistrationService(formData);
        setName('');
        setLastName('');
        setUserName('');
        setEmail('');
        setPassword('');      
        setDateOfBirth('');
        setAddress('');
        setPicture(null);
        setRole('');

        console.log(JSON.stringify(formData));
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
    <div>
    
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
      <input type="file" onChange={handleFileChange} />
      <button onClick={handleFileUpload}>Set picture</button>
    </div>
    
     
    <button className='btn' type="submit"> Registruj se</button>
    <br/>
    </form>
    
   
    </div>


  );     
};

export default Registration;