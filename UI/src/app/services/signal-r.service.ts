import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { ChatMessageModel } from '../Interfaces/ChatMessageModel'

const HOST_CHAT = 'http://localhost:5000/work'

export class SignalRService {
  public data: ChatMessageModel[];

  private hubConnection: signalR.HubConnection
  private employerId: number;

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
        console.log("Kke", this.employerId);
        this.hubConnection.invoke('JoinRoom', this.employerId)
      })
      .catch(err => console.log(err))
  }

  public dropConnection(): void {
    this.hubConnection.invoke('LeaveRoom', this.employerId)
    this.hubConnection.stop()
  }

  public sendMessage(message: string): void {
    this.hubConnection.invoke('SendMessage', localStorage.get('accessToken'), this.employerId, message)
  }

  public addTransferChartDataListener = () => {
    this.hubConnection.on('ReceiveMessage', (data) => {
      console.log(data);
    });
  }
}
