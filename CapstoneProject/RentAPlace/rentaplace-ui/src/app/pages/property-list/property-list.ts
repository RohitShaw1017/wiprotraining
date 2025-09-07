import { Component, OnInit } from '@angular/core';
import { Property,type PropertyVM, type PropertyImageVM } from '../../services/property';

@Component({
  selector: 'app-property-list',
  standalone: false,
  templateUrl: './property-list.html',
  styleUrls: ['./property-list.css'] // <-- plural
})
export class PropertyList implements OnInit {
  properties: any[] = [];
  loading = true;
  error = '';

  location: string = '';
  type: string = '';
  features: string = '';

  constructor(public property: Property) {}

   async ngOnInit() {
    await this.load(); // load all initially
  }
   async load(filters?: { location?: string; type?: string; features?: string }) {
    this.loading = true;
    try {
      this.properties = await this.property.getAll();
      console.log('properties:', this.properties);
    } catch (e: any) {
      console.error('Error loading properties:', e);
      this.error = e?.message || 'Failed to load properties';
    } finally {
      this.loading = false;
    }
  }
  async executeSearch() {
    await this.load({
      location: this.location,
      type: this.type,
      features: this.features
    });
  }
  async clearSearch() {
    this.location = '';
    this.type = '';
    this.features = '';
    await this.load();
  }
  featuresOf(p: PropertyVM): string[] {
    if (!p?.features) return [];
    return p.features.split(',').map(s => s.trim()).filter(Boolean);
  }
   trackByImg(_index: number, img: PropertyImageVM) {
    return img.imageId;
  }
}
