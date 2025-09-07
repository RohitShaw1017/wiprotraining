import { Injectable } from '@angular/core';
import api from './api';

export type MessageVM = {
  messageId: number;
  senderId: number;
  senderName?: string;
  receiverId: number;
  receiverName?: string;
  propertyId?: number | null;
  messageText: string;
  createdAt: string;
  replyText?: string | null;
  replyAt?: string | null;
  isReadByReceiver?: boolean;
};

@Injectable({
  providedIn: 'root'
})
export class Message {
  private base = '/messages';

  // renter sends message to owner
  async send(dto: { receiverId: number; propertyId?: number | null; messageText: string }) {
    const res = await api.post(`${this.base}/send`, dto);
    return res.data;
  }

  // messages for current user
  async myMessages(): Promise<MessageVM[]> {
    const res = await api.get(`${this.base}/my`);
    return res.data ?? [];
  }

  // owner's messages for a property
  async forProperty(propertyId: number): Promise<MessageVM[]> {
    const res = await api.get(`${this.base}/property/${propertyId}`);
    return res.data ?? [];
  }

  // reply (owner)
  async reply(messageId: number, replyText: string) {
    const res = await api.put(`${this.base}/${messageId}/reply`, { replyText });
    return res.data;
  }

  // mark read
  async markRead(messageId: number) {
    const res = await api.put(`${this.base}/${messageId}/read`);
    return res.data;
  }
}
