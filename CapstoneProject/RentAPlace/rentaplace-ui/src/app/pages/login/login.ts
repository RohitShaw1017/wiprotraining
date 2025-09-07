import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Auth } from '../../services/auth';
import { setToken } from '../../store/token.actions';
import { extractRoleFromJwt } from '../../utils/jwt';
@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  email = '';
  password = '';

 constructor(private auth : Auth, private store: Store, private router: Router) {}

  async login() {
    try {
      const res = await this.auth.login({
        email: this.email,
        password: this.password
      });
      const token: string = res.token;
      const role = extractRoleFromJwt(token);
      
      // persist for navbar visibility + axios
      localStorage.setItem('token', token);
      if (role) localStorage.setItem('role', role); else localStorage.removeItem('role');

      // keep your existing token store usage
      this.store.dispatch(setToken({ token }));

      alert('Login successful!');
      this.router.navigate(['/']);
    } catch (e: any) {
      console.error('Login error:', e);
      alert('Login failed: ' + (e?.message || 'Unknown error'));
    }
  }
}