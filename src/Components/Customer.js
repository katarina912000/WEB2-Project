
import { useNavigate } from 'react-router-dom';
import { Link, Routes, useHref } from 'react-router-dom';


const Customer = () => 
{
    return(
        <div className="first">
        <p className='paragrafLevi'>zdravo kupče!</p>
        <Link to="/profile2">
                <button  className="custom-button">Profil korisnika</button>
            </Link>
            <Link to="/newOrder">
                <button  className="custom-button">Poruči nesto</button>
            </Link>
        </div>
    );
};
export default Customer;