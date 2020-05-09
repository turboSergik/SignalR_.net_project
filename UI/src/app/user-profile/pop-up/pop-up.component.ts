import {Component, Inject, OnInit} from '@angular/core';
import {JobService} from '../../_services/job.service';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {AddJobData} from '../../_models/AddJobData';
import {PopService} from '../../_services/pop.service';

@Component({
  selector: 'app-pop-up',
  templateUrl: './pop-up.component.html',
  styleUrls: ['./pop-up.component.css']
})
export class PopUpComponent  {

  constructor(@Inject(MAT_DIALOG_DATA) public jobData: AddJobData,
              public dialogRef: MatDialogRef<PopUpComponent>,
              private popupService: PopService) { }

    DeleteEvent(){
    this.popupService.popUp.next(this.jobData.id);
    this.dialogRef.close();
  }
}
