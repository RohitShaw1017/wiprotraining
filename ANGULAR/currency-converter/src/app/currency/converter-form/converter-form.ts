import { Component, Output, EventEmitter } from '@angular/core';
import { Currency } from '../currency';
@Component({
  selector: 'app-converter-form',
  standalone: false,
  templateUrl: './converter-form.html',
  styleUrl: './converter-form.css'
})
export class ConverterForm {

  @Output() convert = new EventEmitter<any>();

  amount = 1;
  from = 'USD';
  to = 'INR';

  result = '';

  currencies = ['USD', 'EUR', 'INR'];
  rates: any = {};

  constructor(private currency: Currency) {
    this.currency.getRates(5).subscribe(data => {
      this.rates = data;
    });
   }

    onSubmit() {
    if (this.rates[this.from] && this.rates[this.to]) {
      const converted = (this.amount / this.rates[this.from]) * this.rates[this.to];
      this.result = `${this.amount} ${this.from} = ${converted.toFixed(2)} ${this.to}`;

      // Emit the result so parent components can use it
      this.convert.emit(this.result);

      this.currency.saveHistory({
        date: new Date(),
        from: this.from,
        to: this.to,
        amount: this.amount,
        result: converted
      }).subscribe();
    }
  }


}
