import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import './Components/Registration.js/registration.css'
import App from './App';
import reportWebVitals from './reportWebVitals';
import Registration from './Components/Registration.js/Registration';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Registration />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
