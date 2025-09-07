
import { Component, OnInit } from '@angular/core';
import { Property, type PropertyVM } from '../../services/property';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit {
  featured: PropertyVM[] = [];
  loading = true;
  error = '';

  role: string | null = null;
  hasToken = false;

  constructor(private property: Property, private router: Router) {}

  async ngOnInit() {

    this.role = localStorage.getItem('role');
    this.hasToken = !!localStorage.getItem('token');

    try {
      const all = await this.property.getAll();
      // pick top 6 (you can sort by rating/date if you like)
      this.featured = all.slice(0, 6);
    } catch (e: any) {
      this.error = e?.message || 'Failed to load properties';
    } finally {
      this.loading = false;
    }
  }
  
   isOwner(): boolean {
    return this.role === 'Owner';
  }

  toFeatureChips(p: PropertyVM): string[] {
    return p.features
      ? p.features.split(',').map(s => s.trim()).filter(Boolean).slice(0, 3)
      : [];
  }

  view(p: PropertyVM) {
    this.router.navigate(['/property', p.propertyId]);
  }
}