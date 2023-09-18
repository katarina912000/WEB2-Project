import {ax} from '../Axios.js'

export const NoviArtikal = async(formData) =>{
    
    console.log(formData);
    const response = await ax.post(`/Product/dodajArtikal`, formData, 
    {headers: 
        {
          "Content-Type":"multipart/form-data",
        }
      });
    return response.data;
};
export const AzurirajArtikal = async(id, formData) => {

  return await ax.put(`/Product/${id}`, formData,
  {headers: 
      {
        "Content-Type":"multipart/form-data",
      }
    }
  );
};

export const ObrisiArtikal = async(id) => {

  const token = localStorage.getItem("jwtToken");
  ax.defaults.headers.common["Authorization"] = `Bearer ${token}`;

  return await ax.delete(`/Product/${id}`);
};


export const SviArtikli = async() => {
  const token = localStorage.getItem("jwtToken");
  ax.defaults.headers.common["Authorization"] = `Bearer ${token}`;

  
  return await ax.get(`/Product/sviArtikli`);
};

export const SviArtikliProdavca = async(id) => {
  const token = localStorage.getItem("jwtToken");
  ax.defaults.headers.common["Authorization"] = `Bearer ${token}`;

  
  return await ax(`/Product/dobaviArtikleProdavca/${id}`);
};