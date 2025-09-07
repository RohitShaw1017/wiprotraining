import { Component,OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Message, MessageVM } from '../../services/message';

@Component({
  selector: 'app-owner-messages',
  standalone: false,
  templateUrl: './owner-messages.html',
  styleUrl: './owner-messages.css'
})
export class OwnerMessages implements OnInit{
  propertyId!: number;
  messages: MessageVM[] = [];
  loading = false;
  replyMap: Record<number, string> = {};

  constructor(private route: ActivatedRoute, private msg: Message) {}

  async ngOnInit() {
    this.propertyId = Number(this.route.snapshot.paramMap.get('propertyId'));
    if (!this.propertyId) return;
    await this.load();
  }

  async load() {
    this.loading = true;
    try {
      this.messages = await this.msg.forProperty(this.propertyId);
      // init reply map
      this.messages.forEach(m => this.replyMap[m.messageId] = m.replyText ?? '');
    } catch (e) {
      console.error(e);
    } finally {
      this.loading = false;
    }
  }

  async sendReply(messageId: number) {
    const text = (this.replyMap[messageId] ?? '').trim();
    if (!text) { alert('Type reply'); return; }
    try {
      await this.msg.reply(messageId, text);
      alert('Replied');
      await this.load();
    } catch (e) {
      console.error(e);
      alert('Reply failed');
    }
  }
}
