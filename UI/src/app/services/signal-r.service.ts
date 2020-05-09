import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { ChatMessageModel } from '../Interfaces/ChatMessageModel'

const HOST_CHAT = 'http://localhost:5000/work'

export class SignalRService {
  public data: ChatMessageModel[];

  private hubConnection: signalR.HubConnection

  public startConnection = (hubId) => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(HOST_CHAT + '/' + hubId)
      .build();

    this.hubConnection
      .start()
      .then(() => {
        this.hubConnection.invoke('SendMessage', localStorage.getItem('accessToken'), 'lol')
      })
      .catch(err => console.log(err))

    this.hubConnection.on('ReceiveMessage', (data) => {
      console.log(data);
    });
  }

  public dropConnection(): void {
    this.hubConnection.stop();
  }

  public addTransferChartDataListener = () => {
    this.hubConnection.on('ReceiveMessage', (data) => {
      console.log(data);
    });
  }
}
