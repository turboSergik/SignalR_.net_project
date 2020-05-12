import {Component, Inject, Input, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import { SignalRService } from "../services/signal-r.service"
import {ChatMessageModel} from '../Interfaces/ChatMessageModel';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy {
  closed: boolean = true;

  public receivedMessages: ChatMessageModel[] = [];
  conn: SignalRService;
  inputValue;

  /*
    Building modal window
    employerId incoming from openChat func in job.component
   */
  constructor(private formBuilder: FormBuilder,
              @Inject(MAT_DIALOG_DATA) public employerId: number) {
    this.inputValue = this.formBuilder.group({
      text: ''
    });
  }

  /*
    When modal initialized this func should calling
    we connecting to data provider and set handler for "ReceivedMessage" event
   */
  ngOnInit(): void {
    this.conn = new SignalRService(this.employerId);
    this.conn.startConnection()
    this.conn.onMessageReceived(([ username, employer, message ]) => {
      this.receivedMessages.push({ username, employer, message })
    })
  }

  /*
    Drop connection if window closed
   */
  ngOnDestroy(): void {
    this.conn.dropConnection()
  }

  /*
    If user submit form, this func will be called
    value - user input
    we send message to data provider (api) and reset form
   */
  onSubmit(value) {
    this.conn.sendMessage(value.text)
    this.inputValue.reset()
  }
}
