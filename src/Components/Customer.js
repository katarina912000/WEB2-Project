
import { useNavigate } from 'react-router-dom';
import { Link, Routes, useHref } from 'react-router-dom';


const Customer = () => 
{
    return(
        <div className="first">
        <p>zdravo kupce!</p>
        <Link to="/profile2">
                <button  className="custom-button">Profil korisnika</button>
            </Link>
        </div>
    );
};
export default Customer;