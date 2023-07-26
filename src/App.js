import React from "react";
import Registration from "./Components/Registration.js/Registration";
import { Link } from 'react-router-dom';

function App() {
  return (
    <div>
    {/* Link to the desired route */}
    <Link to="/registration">
      <button>Go to registration</button>
    </Link>

    {/* You can also use Link with a custom component */}
    <Link to="/registration" component={Registration}>
    registration
    </Link>
    <a href="http://localhost:3001/registration">
        Registration
    </a>
    <h1>
      sta se desava
    </h1>
   </div>
   
  
    
  );
}

export default App;
