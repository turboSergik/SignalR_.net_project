import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import { SignalRService } from "../services/signal-r.service"
import {ChatMessageModel} from '../Interfaces/ChatMessageModel';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  closed: boolean = true;
  @Input() employerId: number;

  receivedMessages: [{ jwtToken: '', message: "kek" }];
  conn: SignalRService;
  inputValue;

  constructor(private formBuilder: FormBuilder) {
    this.inputValue = this.formBuilder.group({
      text: ''
    });
  }

  ngOnInit(): void {
    this.conn = new SignalRService(this.employerId);
  }

  toggle() {
    if (this.closed === true) {
      this.conn.startConnection()
      this.conn.onMessageReceived((data) => {
        console.log(data);
        this.receivedMessages.push({ jwtToken: null, message: data[1] })
      })
    } else {
      this.conn.dropConnection()
    }

    return this.closed = !this.closed
  }

  onSubmit(value) {
    this.conn.sendMessage(value.text)
    this.inputValue.reset()
  }
}
