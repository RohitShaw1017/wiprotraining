import { Component, signal } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { clearToken } from './store/token.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App {
  year = new Date().getFullYear();
   isLoggedIn() {
    return !!localStorage.getItem('token');
  }

  isOwner() {
    return localStorage.getItem('role') === 'Owner';
  }
  constructor(private router: Router, private store: Store) {}

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.store.dispatch(clearToken()); // clears your token slice
    this.router.navigate(['/']);
  }
}

