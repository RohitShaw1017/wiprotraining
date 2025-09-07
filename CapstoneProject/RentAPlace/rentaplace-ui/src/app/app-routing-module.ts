import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Home } from './pages/home/home';
import { PropertyList } from './pages/property-list/property-list';
import { PropertyDetail } from './pages/property-detail/property-detail';
import { OwnerDashboard } from './pages/owner-dashboard/owner-dashboard';
import { OwnerReservations } from './pages/owner-reservations/owner-reservations';
import { OwnerMessages } from './pages/owner-messages/owner-messages';

const routes: Routes = [
  { path: '', component: Home },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'properties', component: PropertyList },
  { path: 'property/:id', component: PropertyDetail },
  { path: 'owner-dashboard', component: OwnerDashboard },
  { path: 'owner-reservations', component: OwnerReservations },
  { path: 'owner/messages/:propertyId', component: OwnerMessages },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
