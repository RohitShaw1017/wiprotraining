import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-conversion-result',
  standalone: false,
  templateUrl: './conversion-result.html',
  styleUrl: './conversion-result.css'
})
export class ConversionResult {

  @Input() result!: string;

}
