import { Component,OnInit } from '@angular/core';
import { Property,type PropertyVM } from '../../services/property';

@Component({
  selector: 'app-owner-dashboard',
  standalone: false,
  templateUrl: './owner-dashboard.html',
  styleUrls: ['./owner-dashboard.css']
})
export class OwnerDashboard {
  title = '';
  description = '';
  location = '';
  type = '';
  features = '';
  pricePerNight: number | null = null;

  selectedFiles: File[] = [];
  previews: string[] = [];
  uploading = false;

   // Owner properties list
  myProperties: PropertyVM[] = [];
  loading = false;
  error = '';

  // editing state
  editingId: number | null = null;
  editModel: Partial<PropertyVM> = {};
  // per-property upload file inputs (not global)
  perPropSelectedFiles: { [propId: number]: File[] } = {};

  constructor(private property: Property) {}

  

  onFilesChanged(event: Event) {
    const input = event.target as HTMLInputElement;
    const files = input.files ? Array.from(input.files) : [];
    this.selectedFiles = files;

    // show small previews
    this.previews = [];
    for (const f of files) {
      const reader = new FileReader();
      reader.onload = (e: any) => this.previews.push(e.target.result as string);
      reader.readAsDataURL(f);
    }
  }

  async addProperty() {
    try {
      this.uploading = true;

      const created = await this.property.addProperty({
        title: this.title,
        description: this.description,
        location: this.location,
        type: this.type,
        features: this.features,
        pricePerNight: this.pricePerNight
      });
      
      if (created?.propertyId && this.selectedFiles.length) {
        for (const f of this.selectedFiles) {
          await this.property.uploadImage(created.propertyId, f);
        }
      }

      alert('✅ Property added successfully!');
      this.clearForm();
    } catch (err) {
      console.error('Error adding property:', err);
      alert('❌ Failed to add property. Did you login as Owner?');
    }finally {
      this.uploading = false;
    }
  }

  clearForm() {
    this.title = '';
    this.description = '';
    this.location = '';
    this.type = '';
    this.features = '';
    this.pricePerNight = null;
    this.selectedFiles = [];
    this.previews = [];
  }

ngOnInit() {
    this.loadMyProperties();
  }

  async loadMyProperties() {
  this.loading = true;
  this.error = '';
  try {
    console.log('Calling GET /api/properties/owner — token present?', !!localStorage.getItem('token'));
    const data = await this.property.getByOwner(); // your service method
    console.log('owner props:', data);
    this.myProperties = data;
  } catch (err: any) {
    // NEW: better debug info
    console.error('Failed loading owner properties — error object:', err);
    if (err?.response) {
      console.error('err.response.status:', err.response.status);
      console.error('err.response.headers:', err.response.headers);
      console.error('err.response.data:', err.response.data);
      this.error = `Request failed with status code ${err.response.status}`;
    } else {
      this.error = err?.message || 'Failed to load properties';
    }
  } finally {
    this.loading = false;
  }
}
  // ----- EDIT / UPDATE -----
  startEdit(p: PropertyVM) {
    this.editingId = p.propertyId;
    // shallow copy
    this.editModel = {
      title: p.title,
      description: p.description,
      location: p.location,
      type: p.type,
      features: p.features,
      pricePerNight: p.pricePerNight
    };
  }
   cancelEdit() {
    this.editingId = null;
    this.editModel = {};
  }
  async saveEdit() {
    if (!this.editingId) return;
    try {
      const payload = {
        title: this.editModel.title,
        description: this.editModel.description,
        location: this.editModel.location,
        type: this.editModel.type,
        features: this.editModel.features,
        pricePerNight: this.editModel.pricePerNight
      };
      await this.property.updateProperty(this.editingId, payload);
      alert('Updated');
      this.cancelEdit();
      await this.loadMyProperties();
    } catch (e) {
      console.error('Update failed', e);
      alert('Failed to update property');
    }
  }
  // ----- DELETE -----
  async deleteProperty(id: number) {
    if (!confirm('Delete this property?')) return;
    try {
      await this.property.deleteProperty(id);
      alert('Deleted');
      await this.loadMyProperties();
    } catch (e) {
      console.error('Delete failed', e);
      alert('Failed to delete property');
    }
  }
   // ----- UPLOAD PHOTOS FOR AN EXISTING PROPERTY -----
  onPerPropFilesChanged(event: Event, propId: number) {
    const input = event.target as HTMLInputElement | null;
    const files = input?.files ? Array.from(input.files) : [];
    this.perPropSelectedFiles[propId] = files;
  }

  async uploadImagesForProperty(propId: number) {
    const files = this.perPropSelectedFiles[propId] ?? [];
    if (!files.length) { alert('No files selected'); return; }
    if (files.length > 6) { alert('Max 6 images at a time'); return; }

    try {
      for (const f of files) {
        await this.property.uploadImage(propId, f);
      }
      alert('Uploaded');
      // clear input selection and reload list
      this.perPropSelectedFiles[propId] = [];
      await this.loadMyProperties();
    } catch (e) {
      console.error('Upload failed', e);
      alert('Failed to upload images');
    }
  }

  // small helper to read features into array
  splitFeatures(p: PropertyVM) {
    return p.features ? p.features.split(',').map(x => x.trim()).filter(x => x) : [];
  }
}
