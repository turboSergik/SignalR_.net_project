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

  /*
    Func witch connecting with remote data provider from HOST_CHAT
    Steps:
      1. Connecting to data provider
      2. Register into room with name == this.employerId
   */
  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(HOST_CHAT)
      .build();

    this.hubConnection
      .start()
      .then(() => {
        this.hubConnection.invoke('JoinRoom', this.employerId, localStorage.getItem('accessToken'))
      })
      .catch(err => console.log(err))
  }

  public onEmployerOnline = (cb) => {
    this.hubConnection.on('OnEmployerOnline', (...data) => {
      cb(data)
    })
  }

  public onEmployerOffline = (cb) => {
    this.hubConnection.on('OnEmployerOffline', (...data) => {
      cb(data)
    })
  }

  /*
    Receive message event handler
   */
  public onMessageReceived = (cb) => {
    this.hubConnection.on('ReceiveMessage', (...data) => {
      cb(data)
    });
  }

  public dropConnection(): void {
    this.hubConnection.invoke('LeaveRoom', this.employerId, localStorage.getItem('accessToken'))
    // this.hubConnection.stop()
  }

  /*
    Send message func
    'SendMessage' - function witch will be called on backend
    localStorage.getItem('accessToken') - jwt witch be placed as 1 argument to func SendMessage
    this.employerId - work identifier, 2 arg
    message - user message, 3 arg
   */
  public sendMessage(message: string): void {
    this.hubConnection.invoke('SendMessage', localStorage.getItem('accessToken'), this.employerId, message)
  }
}
