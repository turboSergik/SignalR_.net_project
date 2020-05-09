import { Injectable } from '@angular/core';
import {Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PopService {
  popUp = new Subject();

  onPopup(){
    return this.popUp.asObservable();
  }
  constructor() { }
}
