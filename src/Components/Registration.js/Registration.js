import react from 'react';
import './registration.css'
function Registration()
{
    return (
        
            <div className='div1'>
                <label>Registration</label>
                <div >email <input type='text'/></div>
                <div >UserName <input type='text'/></div>
                <div>Name <input type='text'/></div>
                <div>name <input type='text'/></div>
                <div>last name <input type='text'/></div>
                <div>password <input type='pasword'/></div>
                <div>Already have an account? Go to <a href='/login'>Login</a></div>

            </div>
                
            
        
    )
}

export default Registration;