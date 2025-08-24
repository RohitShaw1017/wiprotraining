import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { ConverterForm } from './currency/converter-form/converter-form';
import { ConversionResult } from './currency/conversion-result/conversion-result';
import { History } from './currency/history/history';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    App,
    ConverterForm,
    ConversionResult,
    History
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners()
  ],
  bootstrap: [App]
})
export class AppModule { }
