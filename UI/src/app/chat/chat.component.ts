import {Component, HostListener, Inject, Input, OnChanges, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import { SignalRService } from "../services/signal-r.service"
import {ChatMessageModel} from '../Interfaces/ChatMessageModel';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnChanges {
  closed: boolean = true;
  employerOnline: boolean = false;
  @Input() employerId: number;

  public receivedMessages: ChatMessageModel[] = [];
  conn: SignalRService;
  inputValue;

  /*
    Building modal window
    employerId incoming from openChat func in job.component
   */
  constructor(private formBuilder: FormBuilder) {
    this.inputValue = this.formBuilder.group({
      text: ''
    });

  }

  ngOnChanges() {
    if (this.conn) this.conn.dropConnection()

    this.receivedMessages = [];
    this.employerOnline = false;

    this.conn = new SignalRService(this.employerId)
    this.conn.startConnection()
    this.conn.onMessageReceived(([ username, employer, message ]) => {
      this.receivedMessages.push({ username, employer, message })
    })
    this.conn.onEmployerOnline(() => {
      console.log("Employer");
      this.employerOnline = true
    })
    this.conn.onEmployerOffline(() => {
      this.employerOnline = false
    })
  }

  @HostListener('window:beforeunload', [ '$event' ])
  unloadHandler(event): void {
    this.conn.dropConnection()
  }

  toggle(): boolean {
    return this.closed = !this.closed;
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
