import React from "react";
import Registration from "./Registration";
import react, { useState}  from 'react';
import { LoginService } from '../Services/LoginService';
import { Link, Routes, json, useHref } from 'react-router-dom';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

const Login = () => 
{
    const [email, setEmail] = useState('');   
    const [password, setPassword] = useState('');

    const handleSubmit = async(e) =>
    {
        e.preventDefault();

      try{
        console.log(JSON.stringify(email,password));
        console.log(email,password);
        
        const token= await LoginService(email,password);
        
        setEmail('');
        setPassword('');
       // console.log(JSON.stringify(formData));
      }catch(error)
       {
        if (error.response && error.response.data && error.response.data.errors) {
          const errors = error.response.data.errors;
         
          
        } else {
          console.log(error);
        }
        }
    };

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
                <button className='btn' type="submit">Login</button>
                <br />
            </form>
        </div>
        <div>
        <p>
                You dont have account?
            </p>
            <Link to="/registration">
                <button>Get register</button>
            </Link>
            
        </div>
        
      
      
</div>

    );
};
export default Login;