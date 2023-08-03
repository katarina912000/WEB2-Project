import React from "react";
import './App.css'
import {ax} from './Axios.js'
import { useEffect } from "react";
import Registration from "./Components/Registration";
import { Link, Routes, useHref } from 'react-router-dom';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import Login from "./Components/Login";


function App() {
//za token koji se gubi ako se izabere logout
  useEffect(() => {
    const token = localStorage.getItem('jwtToken');
    if (token) {
      ax.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    } else {
      delete ax.defaults.headers.common['Authorization'];
    }
  }, []); // Ovo će se izvršiti samo jednom pri montiranju komponente
  return (
    //ovo ce biti kao home i sve sto je ovde ce se prenositi svima svuda
    <div className="firstF">
      <p>
        Welcome to the online shop site!
      </p>
      <br/>
      <br/>
      <br/>
      <Link to="/login">
                <button  className="custom-button">Log</button>
            </Link>
      

      
      <Routes>
        <Route path="/login" element={<Login />} />
      </Routes>
      <Routes>
        <Route path="/registration" element={<Registration />} />
      </Routes>

    </div>
    
   
  );
}

export default App;
