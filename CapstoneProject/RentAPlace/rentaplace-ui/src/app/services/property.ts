import { Injectable } from '@angular/core';
import api from './api';

// Inline UI types
export type PropertyImageVM = {
  imageId: number;
  imageUrl: string;
};

export type PropertyVM = {
  propertyId: number;
  ownerId: number | null;
  title: string;
  description: string;
  location: string;
  type: string;
  features: string;      // comma-separated string from API
  pricePerNight: number;
  rating?: number;
  createdAt?: string | null;
  images: PropertyImageVM[];
};

@Injectable({ providedIn: 'root' })
export class Property {
  private base = '/properties';
  private apiOrigin = 'http://localhost:5019'; // Kestrel origin for static files

  private toAbs(p: string | null | undefined): string {
    if (!p) return '';
    const s = String(p);
    if (s.startsWith('http://') || s.startsWith('https://')) return s;
    return s.startsWith('/') ? `${this.apiOrigin}${s}` : `${this.apiOrigin}/${s}`;
  }

  async getAll(filters?: { location?: string; type?: string; features?: string }): Promise<PropertyVM[]> {
    const config: any = {};
    if (filters) {
      const params: any = {};
      if (filters.location) params.location = filters.location;
      if (filters.type) params.type = filters.type;
      if (filters.features) params.features = filters.features;
      config.params = params;
    }
    const res = await api.get(this.base); // http://localhost:5019/api/properties
    const data = res.data;
    const arr = Array.isArray(data) ? data : (data ? [data] : []);

    return arr.map((x: any): PropertyVM => ({
      propertyId:     x.propertyId     ?? x.PropertyId     ?? 0,
      ownerId:        x.ownerId        ?? x.OwnerId        ?? null,
      title:          x.title          ?? x.Title          ?? '',
      description:    x.description    ?? x.Description    ?? '',
      location:       x.location       ?? x.Location       ?? '',
      type:           x.type           ?? x.Type           ?? '',
      features:       x.features       ?? x.Features       ?? '',
      pricePerNight:  x.pricePerNight  ?? x.PricePerNight  ?? 0,
      rating:         x.rating         ?? x.Rating         ?? 0,
      createdAt:      x.createdAt      ?? x.CreatedAt      ?? null,
      images: (x.images ?? x.Images ?? []).filter((z: any) => z).map((img: any): PropertyImageVM => ({
        imageId:  img.imageId ?? img.propertyImageId ?? img.ImageId ?? img.PropertyImageId ?? 0,
        imageUrl: this.toAbs(img.imageUrl ?? img.ImageUrl ?? '')
      }))
    }));
  }

  async getById(id: number): Promise<PropertyVM> {
    const res = await api.get(`${this.base}/${id}`);
    const p = res.data;
    return {
      propertyId:     p.propertyId     ?? p.PropertyId     ?? 0,
      ownerId:        p.ownerId        ?? p.OwnerId        ?? null,
      title:          p.title          ?? p.Title          ?? '',
      description:    p.description    ?? p.Description    ?? '',
      location:       p.location       ?? p.Location       ?? '',
      type:           p.type           ?? p.Type           ?? '',
      features:       p.features       ?? p.Features       ?? '',
      pricePerNight:  p.pricePerNight  ?? p.PricePerNight  ?? 0,
      rating:         p.rating         ?? p.Rating         ?? 0,
      createdAt:      p.createdAt      ?? p.CreatedAt      ?? null,
      images: (p.images ?? p.Images ?? []).filter((z: any) => z).map((img: any): PropertyImageVM => ({
        imageId:  img.imageId ?? img.propertyImageId ?? img.ImageId ?? img.PropertyImageId ?? 0,
        imageUrl: this.toAbs(img.imageUrl ?? img.ImageUrl ?? '')
      }))
    };
  }

  async addProperty(data: {
    title: string;
    description: string;
    location: string;
    type: string;
    features: string;
    pricePerNight: number | null;
  }): Promise<PropertyVM> {
    const res = await api.post(this.base, data); // JWT via interceptor
    return res.data;
  }

  async uploadImage(propertyId: number, file: File): Promise<PropertyImageVM> {
    const form = new FormData();
    form.append('file', file);
    const res = await api.post(`${this.base}/${propertyId}/upload`, form, {
      headers: { 'Content-Type': 'multipart/form-data' }
    });
    const img = res.data;
    return {
      imageId:  img.imageId ?? img.propertyImageId ?? img.ImageId ?? img.PropertyImageId ?? 0,
      imageUrl: this.toAbs(img.imageUrl ?? img.ImageUrl ?? '')
    };
  }
  async getByOwner(): Promise<any[]> {
  const res = await api.get('/properties/owner'); // api baseUrl should be http://localhost:5019/api
  console.log('getByOwner response', res.status, res.data);
  const data = res.data;
  const arr = Array.isArray(data) ? data : (data ? [data] : []);
  return arr.map((x: any): PropertyVM => ({
      propertyId:     x.propertyId     ?? x.PropertyId     ?? 0,
      ownerId:        x.ownerId        ?? x.OwnerId        ?? null,
      title:          x.title          ?? x.Title          ?? '',
      description:    x.description    ?? x.Description    ?? '',
      location:       x.location       ?? x.Location       ?? '',
      type:           x.type           ?? x.Type           ?? '',
      features:       x.features       ?? x.Features       ?? '',
      pricePerNight:  x.pricePerNight  ?? x.PricePerNight  ?? 0,
      rating:         x.rating         ?? x.Rating         ?? 0,
      createdAt:      x.createdAt      ?? x.CreatedAt      ?? null,
      images: (x.images ?? x.Images ?? []).filter((z: any) => z).map((img: any): PropertyImageVM => ({
        imageId:  img.imageId ?? img.propertyImageId ?? img.ImageId ?? img.PropertyImageId ?? 0,
        imageUrl: this.toAbs(img.imageUrl ?? img.ImageUrl ?? '')
      }))
    }));
}

async updateProperty(id: number, payload: any) {
  // payload should match PropertyUpdateDto on backend
  const res = await api.put(`/properties/${id}`, payload);
  console.log('updateProperty', res.status, res.data);
  return res.data;
}

async deleteProperty(id: number) {
  const res = await api.delete(`/properties/${id}`);
  console.log('deleteProperty', res.status);
  return;
}

  // helper to keep normalize logic DRY
  private normalizeProperty(x: any): PropertyVM {
    return {
      propertyId:     x.propertyId     ?? x.PropertyId     ?? 0,
      ownerId:        x.ownerId        ?? x.OwnerId        ?? null,
      title:          x.title          ?? x.Title          ?? '',
      description:    x.description    ?? x.Description    ?? '',
      location:       x.location       ?? x.Location       ?? '',
      type:           x.type           ?? x.Type           ?? '',
      features:       x.features       ?? x.Features       ?? '',
      pricePerNight:  x.pricePerNight  ?? x.PricePerNight  ?? 0,
      rating:         x.rating         ?? x.Rating         ?? 0,
      createdAt:      x.createdAt      ?? x.CreatedAt      ?? null,
      images: (x.images ?? x.Images ?? []).filter((z: any) => z).map((img: any): PropertyImageVM => ({
        imageId:  img.imageId ?? img.propertyImageId ?? img.ImageId ?? img.PropertyImageId ?? 0,
        imageUrl: this.toAbs(img.imageUrl ?? img.ImageUrl ?? '')
      }))
    };
  }
}
