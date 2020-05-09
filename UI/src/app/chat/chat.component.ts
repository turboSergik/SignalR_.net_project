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
  inputValue;

  constructor(private formBuilder: FormBuilder) {
    this.inputValue = this.formBuilder.group({
      text: ''
    });
  }

  ngOnInit(): void {
    const conn = new SignalRService();
    conn.startConnection("");
  }

  connectWithServer(): void {

  }

  toggle() {
    return this.closed = !this.closed
  }

  onSubmit(value) {
    this.inputValue.reset()
  }
}
