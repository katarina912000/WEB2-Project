import React from "react";
import Registration from "./Components/Registration.js/Registration";
import { Link, Routes, useHref } from 'react-router-dom';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';


function App() {
  return (
    <div style={{ backgroundColor: 'black'}}>
      {/* Link ka /registration ruti */}
      <Link to="/registration">
        <button>Registracija</button>
      </Link>

      {/* Definicija ruta */}
      <Routes>
        <Route path="/registration" element={<Registration />} />
      </Routes>
    </div>
   
  );
}

export default App;
