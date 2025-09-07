import { Injectable } from '@angular/core';
import api from './api';

export type OwnerReservationVM = {
  reservationId: number;
  propertyId: number;
  propertyTitle: string;
  propertyImageUrl?: string;
  renterName: string;
  renterEmail: string;
  checkInDate: string;
  checkOutDate: string;
  status: string;
  createdAt: string;
};

@Injectable({ providedIn: 'root' })
export class OwnerReservation {
  private base = '/reservations';

  async getForOwner(): Promise<OwnerReservationVM[]> {
    const res = await api.get(`${this.base}/owner`);
    return res.data;
  }

  async updateStatus(reservationId: number, status: 'Accepted' | 'Rejected') {
    // PUT body is a string in our backend. send JSON body.
    const res = await api.put(`/reservations/${reservationId}/status`, { status });
    
    return res.data;
  }
}
