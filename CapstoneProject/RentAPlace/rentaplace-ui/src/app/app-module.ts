import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Home } from './pages/home/home';
import { PropertyList } from './pages/property-list/property-list';
import { PropertyDetail } from './pages/property-detail/property-detail';
import { OwnerDashboard } from './pages/owner-dashboard/owner-dashboard';

import { FormsModule } from '@angular/forms';
import { StoreModule } from '@ngrx/store';
import { tokenReducer } from './store/token.reducer';
import { OwnerReservations } from './pages/owner-reservations/owner-reservations';
import { OwnerMessages } from './pages/owner-messages/owner-messages';
@NgModule({
  declarations: [
    App,
    Login,
    Register,
    Home,
    PropertyList,
    PropertyDetail,
    OwnerDashboard,
    OwnerReservations,
    OwnerMessages
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    StoreModule.forRoot({ token: tokenReducer }) 

  ],
  providers: [
    provideBrowserGlobalErrorListeners()
  ],
  bootstrap: [App]
})
export class AppModule { }
