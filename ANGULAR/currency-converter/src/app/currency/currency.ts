import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, timer, switchMap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Currency {

  private apiUrl = 'http://localhost:3001';

  constructor(private http: HttpClient) { }

  getRates(intervalSec: number = 10): Observable<any> {
    return timer(0, intervalSec * 1000).pipe(
      switchMap(() => this.http.get(`${this.apiUrl}/rates`))
    );
  }

  // Save conversion history
  saveHistory(entry: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/history`, entry);
  }

  // Get history
  getHistory(): Observable<any> {
    return this.http.get(`${this.apiUrl}/history`);
  }
  
}
