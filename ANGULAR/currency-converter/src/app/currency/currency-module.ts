import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CurrencyRoutingModule } from './currency-routing-module';
import { ConverterForm } from './converter-form/converter-form';
import { ConversionResult } from './conversion-result/conversion-result';
import { History } from './history/history';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    CurrencyRoutingModule,
    HttpClientModule,
    FormsModule
    
  ]
})
export class CurrencyModule { }
