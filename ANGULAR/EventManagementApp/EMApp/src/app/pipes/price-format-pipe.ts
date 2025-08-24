import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'priceFormat',
  standalone: false
})
export class PriceFormatPipe implements PipeTransform {

 transform(value: number): string {
    if (value == null || isNaN(value)) return '₹0.00';
    // Use Intl.NumberFormat for proper grouping
    const nf = new Intl.NumberFormat('en-IN', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    return `₹${nf.format(value)}`;
  }

}
