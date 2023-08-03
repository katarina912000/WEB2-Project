import {ax} from '../Axios.js'

export const LoginService = async(email,password) =>
{
    console.log(JSON.stringify(email,password));
    console.log(email,password);
    const response = await ax.post('/login',
    {
        email: email,
        password: password,
      },       
    {
        headers:
        {"Content-Type": "application/json"},
    });
    
    
    console.log(response.data);
    const token = response.data.token; // Pretpostavlja se da se token vraća kao odgovor s ključem 'token'
    localStorage.setItem('jwtToken', token);

    // Postaviti token u Axios header za sve buduće zahtjeve
    ax.defaults.headers.common['Authorization'] = `Bearer ${token}`;

    return response.data;//trebalo bi token da vrati
    //trebalo bi u headersu da posaljemo token isto ako ga vec ima
    
};