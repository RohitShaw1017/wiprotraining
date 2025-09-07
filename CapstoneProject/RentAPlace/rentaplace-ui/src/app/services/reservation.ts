import { Injectable } from '@angular/core';
import api from './api';

export type ReservationCreateDto = {
  propertyId: number;
  checkInDate: string;  // yyyy-mm-dd
  checkOutDate: string;
};

@Injectable({ providedIn: 'root' })
export class Reservation {
  private base = '/reservations';

  async create(dto: ReservationCreateDto) {
    // api is your axios instance that should attach Authorization header
    const res = await api.post(this.base, dto);
    return res.data;

    // async getMine() {
    // const res = await api.get(this.base + '/mine');
    // return res.data;
  }
}
