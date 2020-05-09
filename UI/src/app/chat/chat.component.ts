import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import { SignalRService } from "../services/signal-r.service"

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  closed: boolean = true;
  @Input() employerId: number;
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
    } else {
      this.conn.dropConnection()
    }

    return this.closed = !this.closed
  }

  onSubmit(value) {
    this.inputValue.reset()
  }
}
