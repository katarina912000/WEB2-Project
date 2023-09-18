import {ax} from '../Axios.js'

export const LoginService = async(email,password) =>
{
    try {
    const response = await ax.post('/login',
    JSON.stringify({ email, password }),       
    {
        headers:
        {"Content-Type": "application/json"},
    });
    
    
    console.log(response);
    const token = response.data; // Pretpostavlja se da se token vraća kao odgovor s ključem 'token'
    localStorage.setItem('jwtToken', token);
    sessionStorage.setItem('jwtToken', token);
    console.log(token);

    

    // Postaviti token u Axios header za sve buduće zahtjeve
    ax.defaults.headers.common['Authorization'] = `Bearer ${token}`;

    return response.data;//trebalo bi token da vrati
    //trebalo bi u headersu da posaljemo token isto ako ga vec ima
} catch (err) {
    alert("Greska prilikom logovanja");
    return null;
  }
    
};

export const setHeader = (token) =>
{

  if(token) 
  {
    ax.defaults.headers.common['Authorization'] = `Bearer ${token}`;


  }  else{
    delete ax.defaults.headers.common['Authorization'];
  }
};

export const googleLogovanje = async (data) => {

  return await ax.post(`/googleLogovanje`, 
   data,
   {headers: 
    {
      "Content-Type":"multipart/form-data"
    }
  });
};