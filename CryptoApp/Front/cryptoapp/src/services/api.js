// src/services/api.js
import axios from 'axios'

const api = axios.create({
  baseURL: 'http://localhost:5127' // Cambialo por el puerto que te muestra la consola
})


export default api
