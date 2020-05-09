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
      .then(() => console.log('Connection started'))
      .catch(err => console.log(err))
  }

  public addTransferChartDataListener = () => {
    this.hubConnection.on('transferchartdata', (data) => {
      this.data = data;
      console.log(data);
    });
  }
}
