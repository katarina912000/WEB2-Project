import {ax} from '../Axios.js'

export const RegistrationService = async(formData) =>
{
    const response = await ax.post('/registration',formData,{
        headers:
        {"Content-Type":"multipart/form-data"},
    });
    // Prikazujemo formData u konzoli pre slanja
    for (let pair of formData.entries()) {
        console.log(`${pair[0]}: ${pair[1]}`);
      }
    console.log(response.data);

    return response.data;
};


