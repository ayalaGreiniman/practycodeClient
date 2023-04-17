import axios from 'axios';
//import { useEffect, useState } from 'react';
//const apiUrl = "http://localhost:5003"
const instance=axios.create({
  baseURL:"http://localhost:5003"
})

instance.interceptors.response.use(
  response => response,
  error => {
    console.error(error);
    return Promise.reject(error);
  }
)

export default {
  getTasks: async () => {
    const result = await instance.get(`/items`)    
    return result.data;
  },

  addTask: async(name)=>{
    console.log('addTask', name);
    // const result = await axios.post(`${apiUrl}/items`,name);
    //const result = await instance.post(`/items?name=${name}`);

    return name;
  },

  setCompleted: async(id, isComplete)=>{
    console.log('setCompleted', {id, isComplete})
    await instance.put(`/items/${id}`) ;   
  },

  deleteTask:async(id)=>{
    console.log('deleteTask')
    await instance.delete(`/items/${id}`)    
  }
};
