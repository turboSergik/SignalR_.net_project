import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { ChatMessageModel } from '../Interfaces/ChatMessageModel'

const HOST_CHAT = 'http://localhost:5000/work'

export class SignalRService {
  private hubConnection: signalR.HubConnection
  private readonly employerId: number;

  constructor(employerId: number) {
    this.employerId = employerId
  }

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(HOST_CHAT)
      .build();

    this.hubConnection
      .start()
      .then(() => {
        this.hubConnection.invoke('JoinRoom', this.employerId)
      })
      .catch(err => console.log(err))
  }

  public onMessageReceived = (cb) => {
    this.hubConnection.on('ReceiveMessage', (...data) => {
      cb(data)
    });
  }

  public dropConnection(): void {
    this.hubConnection.invoke('LeaveRoom', this.employerId)
    this.hubConnection.stop()
  }

  public sendMessage(message: string): void {
    this.hubConnection.invoke('SendMessage', localStorage.getItem('accessToken'), this.employerId, message)
  }
}
