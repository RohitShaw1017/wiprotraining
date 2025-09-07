// src/app/services/api.ts
import axios, { AxiosHeaders, InternalAxiosRequestConfig } from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5019/api',
});

api.interceptors.request.use((config: InternalAxiosRequestConfig) => {
  const path = (config.url || '').toLowerCase();
  const isAuthEndpoint = path.startsWith('/auth');
  if (!isAuthEndpoint) {
  const token = localStorage.getItem('token');
  if (token) {
    // ensure headers is an AxiosHeaders, then set Authorization
    config.headers = config.headers instanceof AxiosHeaders
      ? config.headers
      : new AxiosHeaders(config.headers);

    (config.headers as AxiosHeaders).set('Authorization', `Bearer ${token}`);
  }
}
  return config;
});

export default api;
