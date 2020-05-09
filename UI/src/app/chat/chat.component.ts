import { Component, OnInit } from '@angular/core';
import {FormBuilder} from "@angular/forms";
import { SignalRService } from "../services/signal-r.service"

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  closed: boolean = true;
  conn: SignalRService;
  inputValue;

  constructor(private formBuilder: FormBuilder) {
    this.inputValue = this.formBuilder.group({
      text: ''
    });
    this.conn = new SignalRService();

  }

  ngOnInit(): void {
  }

  connectWithServer(): void {

  }

  toggle() {
    if (this.closed === true) {
      this.conn.startConnection("1")
    } else {
      this.conn.dropConnection()
    }

    return this.closed = !this.closed
  }

  onSubmit(value) {
    this.inputValue.reset()
  }
}
