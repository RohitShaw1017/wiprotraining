import { Component } from '@angular/core';
import { trigger,query, state, style, animate, transition, stagger } from '@angular/animations';
import { Event } from '../../models/event';

@Component({
  selector: 'app-event-list',
  standalone: false,
  templateUrl: './event-list.html',
  styleUrl: './event-list.css',
  animations: [
    trigger('listAnimation', [
      transition('* => *', [
        query(':enter', [
          style({ opacity: 0 }),
          stagger(100, [
            animate('1s ease-in', style({ opacity: 1 }))
          ])
        ], { optional: true })
      ])
    ])
  ]
})

export class EventList {
  events: Event[] = [
    { name: 'Tech Innovators Conference', date: '2025-09-12', price: 3500, category: 'Conference' },
    { name: 'Creative Writing Workshop', date: '2025-10-05', price: 800, category: 'Workshop' },
    { name: 'Rock Music Concert', date: '2025-11-20', price: 2500, category: 'Concert' },
    { name: 'AI & Machine Learning Summit', date: '2025-12-02', price: 5000, category: 'Conference' }
  ];
 formatDate(iso: string) {
    const d = new Date(iso);
    return d.toLocaleDateString('en-GB', { day: '2-digit', month: 'short', year: 'numeric' });
  }
}
