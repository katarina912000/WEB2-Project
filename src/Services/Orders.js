import { ax } from '../Axios.js';



export const SvePorudzbine = async() => {
    const token = localStorage.getItem("jwtToken");
    ax.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    return await ax.get(`/svePorudzbine`);
};


export const NovaPorudzbina = async(reqData) =>{

    const response = await ax.post(`/dodajPorudzbinu`, reqData);
    return response.data;
};



// export const DobaviPorudzbinuPoID = async(id) => {
//     const token = localStorage.getItem("token");
//     ax.defaults.headers.common["Authorization"] = `Bearer ${token}`;

//    return await ax.get(`/${id}`);
// };