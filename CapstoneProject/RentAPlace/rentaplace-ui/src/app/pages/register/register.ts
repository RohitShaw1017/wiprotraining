import { Component } from '@angular/core';
import { Auth } from '../../services/auth';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  name = '';
  email = '';
  password = '';
  role = 'Renter';  // default

  constructor(private auth : Auth,private router: Router) {}

async register() {
    try {
      const res = await this.auth.register({
        name: this.name,
        email: this.email,
        password: this.password,
        role: this.role
      });

      alert('Registration successful! Please login.');
    this.router.navigate(['/login']);
  } catch (e: any) {
    console.error('Registration error:', e);
    alert('Registration failed: ' + (e?.message || 'Unknown error'));
    }
  }
}
