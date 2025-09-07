import { Component, OnInit } from '@angular/core';
import { OwnerReservation, type OwnerReservationVM } from '../../services/owner-reservation';

@Component({
  selector: 'app-owner-reservations',
  standalone: false,
  templateUrl: './owner-reservations.html',
  styleUrls: ['./owner-reservations.css']
  
})
export class OwnerReservations implements OnInit {
  reservations: OwnerReservationVM[] = [];
  loading = false;
  error = '';

  constructor(private svc: OwnerReservation) {}

  async ngOnInit() {
    await this.load();
  }

  async load() {
    this.loading = true;
    try {
      this.reservations = await this.svc.getForOwner();
    } catch (e: any) {
      this.error = e?.message || 'Failed to load';
    } finally {
      this.loading = false;
    }
  }

  async accept(r: OwnerReservationVM) {
    if (!confirm(`Accept reservation #${r.reservationId} by ${r.renterName}?`)) return;
    try {
      await this.svc.updateStatus(r.reservationId, 'Accepted');
      await this.load();
      alert('Reservation accepted.');
    } catch (e) {
      console.error(e);
      alert('Failed to accept.');
    }
  }

  async reject(r: OwnerReservationVM) {
    if (!confirm(`Reject reservation #${r.reservationId} by ${r.renterName}?`)) return;
    try {
      await this.svc.updateStatus(r.reservationId, 'Rejected');
      await this.load();
      alert('Reservation rejected.');
    } catch (e) {
      console.error(e);
      alert('Failed to reject.');
    }
  }
}
