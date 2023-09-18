import React from "react";
import Registration from "./Registration";
import { useState}  from 'react';
import { LoginService,setHeader, googleLogovanje } from '../Services/LoginService';
import { useHistory, useNavigate } from 'react-router-dom';
import { GoogleOAuthProvider } from '@react-oauth/google';
import { Link, Routes, json, useHref } from 'react-router-dom';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import { GoogleLogin } from '@react-oauth/google';
import jwt_decode from "jwt-decode";

const Login = () => 
{
    const [email, setEmail] = useState('');   
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [message, setMessage] = useState('');
    
    const navigate=useNavigate();
    const handleGoogleLogin = async (data) => {
        try {
            localStorage.removeItem('jwtToken');

          const formData = new FormData();
       
          formData.append('googleToken', data.credential);
          console.log(data.credential);
            
          console.log(formData);
          
          const token = await googleLogovanje(formData);
          
          localStorage.setItem('jwtToken', token.data);
          setHeader(token);
          

          navigate('/dashboard');
          const noviProzor = window.open('/dashboard', 'ImeProzora', 'width=600,height=400');

         
        } catch (error) {
          console.log(error);
          setError('Google logovanje nije uspelo');
        }
      };
      
    
      const handleGoogleLoginError = (error) => {
        console.log(error);
        setError('Google logovanje nije uspelo');
      };

    const handleSubmit = async(e) =>
    {
        e.preventDefault();

      try{
        
        localStorage.removeItem('jwtToken');
        const token= await LoginService(email,password);
        if(token !== null)
        {
            console.log(token);
          localStorage.setItem('jwtToken', token);
          setHeader(token);
      
          setEmail('');
        setPassword('');
       // console.log(JSON.stringify(formData));
       setTimeout(() => {
        navigate('/dashboard');
    }, 200);
        }
        
      }catch(error)
       {
        if (error.response && error.response.data && error.response.data.errors) {
          const errors = error.response.data.errors;
         
          
        } else {
          console.log(error);
        }
        }
    };
   


   
    //console.log(userId);

    return (
        <div className="first">
            
            <div>

            
            <h2>Login:</h2>
            <form onSubmit={handleSubmit}>


                <div>
                    <label htmlFor="email">E-mail: </label>
                    <input
                        type="email"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required />
                </div>
                <div>
                    <label htmlFor="password">Lozinka: </label>
                    <input
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required />
                </div>
                <button className="custom-button" type="submit">Login</button>
                <br />
            </form>
        </div>
        <GoogleOAuthProvider clientId="856156717270-agda6bjg6nkd689epao0lhpq8q3363r2.apps.googleusercontent.com">
        <GoogleLogin onSuccess={handleGoogleLogin} onError={handleGoogleLoginError} />
        <br />
        {/* <label className="nazad" htmlFor="/home">
          <Link to="/">Povratak na pocetnu stranicu</Link>
        </label> */}
      </GoogleOAuthProvider>
        {error && <p className="error">{error}</p>}
        <br />
        <label className="nazad" htmlFor="/home">
          <Link to="/">Povratak na početnu stranicu</Link>
        </label>

        <div>
        <p>
               Nemaš nalog? 
            </p>
            <Link to="/registration">
                <button className="custom-button">Registruj se</button>
            </Link>
            
        </div>
        
      
      
</div>

    );
};
export default Login;