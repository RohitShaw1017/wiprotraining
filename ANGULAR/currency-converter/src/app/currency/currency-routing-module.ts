import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ConverterForm } from './converter-form/converter-form';
import { History } from './history/history';

const routes: Routes = [
  { path: 'convert', component: ConverterForm },
  { path: 'history', component: History }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CurrencyRoutingModule { }
