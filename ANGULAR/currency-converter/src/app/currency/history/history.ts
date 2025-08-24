import { Component, OnInit } from '@angular/core';

import { Currency } from '../currency';
@Component({
  selector: 'app-history',
  standalone: false,
  templateUrl: './history.html',
  styleUrl: './history.css'
})
export class History {

  history: any[] = [];

  constructor(private currency: Currency) { } 

  ngOnInit() {
    this.currency.getHistory().subscribe(data => {
      this.history = data;
    });
  }
}
