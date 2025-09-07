import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Property, type PropertyVM, type PropertyImageVM } from '../../services/property';
import { Reservation } from '../../services/reservation';
import { Message } from '../../services/message';
import { extractRoleFromJwt } from '../../utils/jwt';

@Component({
  selector: 'app-property-detail',
  standalone: false,
  templateUrl: './property-detail.html',
  styleUrls: ['./property-detail.css']
})
export class PropertyDetail implements OnInit {
  id!: number;
  property: PropertyVM | null = null;
  loading = true;
  error = '';
  messageText= '';
  sending=false;

  // gallery
  mainImage: string | null = null;

  // reservation form
  checkIn: string = '';
  checkOut: string = '';
  booking = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private propertySvc: Property,
    private reservationSvc: Reservation,
    private msg: Message
  ){}

  isRenter(): boolean {
    const token = localStorage.getItem('token');
    if (!token) return false;
    const role = extractRoleFromJwt(token);
    return role === 'Renter';
  }

  async ngOnInit() {
    const idStr = this.route.snapshot.paramMap.get('id');
    if (!idStr) {
      this.error = 'Invalid property id';
      this.loading = false;
      return;
    }
    this.id = Number(idStr);

    try {
      this.property = await this.propertySvc.getById(this.id);
      // set first image as main
      this.mainImage = this.property?.images?.length ? this.property.images[0].imageUrl : null;
    } catch (e: any) {
      console.error(e);
      this.error = e?.message || 'Failed to load property';
    } finally {
      this.loading = false;
    }
  }

  setMain(img: PropertyImageVM) {
    this.mainImage = img.imageUrl;
    // scroll to top of gallery for UX
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  features(): string[] {
    return this.property?.features ? this.property.features.split(',').map(s => s.trim()).filter(Boolean) : [];
  }

  // Basic local validation
  validDates(): boolean {
    if (!this.checkIn || !this.checkOut) return false;
    const inD = new Date(this.checkIn);
    const outD = new Date(this.checkOut);
    return inD < outD;
  }

  async reserve() {
    if (!this.validDates()) {
      alert('Please choose valid check-in and check-out dates (check-in < check-out).');
      return;
    }
    if (!localStorage.getItem('token')) {
      alert('You must be logged in as a renter to reserve. Please login.');
      this.router.navigate(['/login']);
      return;
    }

    try {
      this.booking = true;
      const res = await this.reservationSvc.create({
        propertyId: this.id,
        checkInDate: this.checkIn,
        checkOutDate: this.checkOut
      });
      alert('Reservation created! Owner will be notified.');
      // optionally redirect to user's reservations page
      this.router.navigate(['/']);
    } catch (err: any) {
      console.error('Reservation error', err);
      alert('Failed to create reservation: ' + (err?.message ?? 'Unknown error'));
    } finally {
      this.booking = false;
    }
  }
      async sendMessage() {
    if (!this.messageText.trim()) { alert('Type a message'); return; }
    if (!this.property?.ownerId) { alert('Owner not found'); return; }

    try {
      this.sending = true;
      await this.msg.send({
        receiverId: this.property?.ownerId,
        propertyId: this.property?.propertyId,
        messageText: this.messageText.trim()
      });

      this.messageText = '';
      alert('Message sent');
    } catch (e: any) {
      console.error(e);
      alert('Failed to send message');
    } finally {
      this.sending = false;
    }
  }
}
