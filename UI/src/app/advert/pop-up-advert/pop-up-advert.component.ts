import {Component, Inject, OnInit} from '@angular/core';
import {AdvertService} from '../../_services/advert.service';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {AddJobData} from '../../_models/AddJobData';
import {AdvertDTO} from '../../_models/DTO/AdvertDTO';

@Component({
  selector: 'app-pop-up-advert',
  templateUrl: './pop-up-advert.component.html',
  styleUrls: ['./pop-up-advert.component.css']
})
export class PopUpAdvertComponent{

  advert: AdvertDTO;
  constructor(private advertServ : AdvertService,
              @Inject(MAT_DIALOG_DATA) public AdvertData: {id:number, title:string},
              public dialogRef: MatDialogRef<PopUpAdvertComponent>,) { }

  DeleteEvent(){
    this.advertServ.popUp.next(this.AdvertData.id);
    this.dialogRef.close();
  }





}
