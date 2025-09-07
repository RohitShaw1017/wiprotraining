import { Injectable } from '@angular/core';
import axios from 'axios';
import api from './api';


@Injectable({ providedIn: 'root' })
export class Auth {
  private base = '/auth';

  async register(data: any) {
    try {
      const res = await api.post(`${this.base}/register`, data);
      return res.data;
    } catch (err) {
      if (axios.isAxiosError(err)) {
        throw new Error(typeof err.response?.data === 'string'
          ? err.response?.data
          : JSON.stringify(err.response?.data || {}));
      }
      throw err;
    }
  }

  async login(data: any) {
    try {
      const res = await api.post(`${this.base}/login`, data);
      return res.data;
    } catch (err) {
      if (axios.isAxiosError(err)) {
        throw new Error(typeof err.response?.data === 'string'
          ? err.response?.data
          : JSON.stringify(err.response?.data || {}));
      }
      throw err;
    }
  }
}
