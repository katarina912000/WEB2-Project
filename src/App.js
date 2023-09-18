import React from "react";
import './App.css'
import {ax} from './Axios.js'
import { useEffect } from "react";
import Registration from "./Components/Registration";
import { Link, Routes, useHref } from 'react-router-dom';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import Dashboard from "./Components/Dashboard";
import Profile2 from "./Components/Profile2";
import Admin from "./Components/Admin";
import Login from "./Components/Login";
import { useHistory, useNavigate } from 'react-router-dom';

import Seller from "./Components/Seller";
import Customer from "./Components/Customer";
import UpdateProfile from "./Components/UpdateProfile";
import AddItem from "./Components/AddItem";
import YourItems from "./Components/YourItems";
import UpdateItem from "./Components/UpdateItem";
import AllOrders from "./Components/AllOrders";
import NewOrder from "./Components/NewOrder";







function App() {
//za token koji se gubi ako se izabere logout
const navigate=useNavigate();

  useEffect(() => {
    const token = localStorage.getItem('jwtToken');
    if (token) {
      ax.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    } else {
      console.log("Katarina upao je u app za brisanje tokena");
      delete ax.defaults.headers.common['Authorization'];
    }
  }, []); // Ovo će se izvršiti samo jednom pri montiranju komponente


const handleLogOut = ()=>{
  localStorage.removeItem('jwtToken');
  console.log("Katarina upao je u deo za Logout za brisanje tokena");
  navigate('/login');
}
  return (
    //ovo ce biti kao home i sve sto je ovde ce se prenositi svima svuda
    <div className="firstF">
      <h2>
       Dobrodošli na onlajn prodavnicu!
      </h2><br></br>
    
      
     
          <div>
          {(localStorage.getItem('jwtToken')!=null) ? <button  className="custom-button"  onClick={handleLogOut}>Log out</button>: <Link to="/login">
          <button  className='custom-button'> Uloguj se</button>
          </Link>}

          
            </div> 
      
      <Routes>
        <Route path="/login" element={<Login />} />
      </Routes>
      <Routes>
        <Route path="/updateItem" element={<UpdateItem />} />
      </Routes>
      <Routes>
        <Route path="/updateProfile" element={<UpdateProfile />} />
      </Routes>
      <Routes>
      <Route path="/allOrders" element={<AllOrders />} />

      </Routes>
      <Routes>
      <Route path="/newOrder" element={<NewOrder />} />

      </Routes>
      <Routes>
        <Route path="/registration" element={<Registration />} />
      </Routes>
      <Routes>
        <Route path="/dashboard" element={<Dashboard />} />
      </Routes>
      <Routes>
        <Route path="/profile2" element={<Profile2 />} />
      </Routes>
      <Routes>
        <Route path="/admin" element={<Admin />} />
      </Routes>
      <Routes>
        <Route path="/seller" element={<Seller />} />
      </Routes>
      <Routes>
        <Route path="/customer" element={<Customer />} />
      </Routes>
      <Routes>
        <Route path="/addItem" element={<AddItem />} />
      </Routes>
      
      <Routes>
        <Route path="/yourItems" element={<YourItems />} />
      </Routes>

      

    </div>
    
   
  );
}

export default App;
