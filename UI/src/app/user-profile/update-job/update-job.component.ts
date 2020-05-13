import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef,} from '@angular/material/dialog';
import {JobService} from '../../_services/job.service';
import {JobDTO} from '../../_models/DTO/JobDTO';
import {AddJobData} from '../../_models/AddJobData';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';


@Component({
  selector: 'app-update-job',
  templateUrl: 'update-job.component.html',
  styleUrls: ['update-job.component.css'],
})
export class UpdateJobComponent implements OnInit {
  job = {} as JobDTO;
  uploadFileName: string;
  profileImage: File = null;

  constructor(
    private jobService: JobService,
    @Inject(MAT_DIALOG_DATA) public jobData: AddJobData,
    public dialogRef: MatDialogRef<UpdateJobComponent>){
  }

  ngOnInit(): void {
    if (this.jobData.id) {
      this.jobService.getJobById(this.jobData.id).subscribe((job: JobDTO) => {
        this.job = job;
        this.job.imagePath = '';
      });
    }
  }

  onJobAdd() {
    let formData: FormData = new FormData();
    formData.append('profileImage', this.profileImage);
    var postData = JSON.stringify(this.job);
    formData.append("postData",postData );

    this.jobService.AddJobFormData(formData).subscribe(() => {
      this.dialogRef.close();
    });
  }

  onJobEdit(job, id) {
    let formData: FormData = new FormData();
    formData.append('profileImage', this.profileImage);
    var postData = JSON.stringify(this.job);
    formData.append("postData",postData );

    this.jobService.editJob(formData, id).subscribe(() => {
      this.dialogRef.close();
    });
  }

  onUploadImage(file) {
    this.profileImage = <File>file.target.files[0];
    this.uploadFileName = file.target.files[0].type.indexOf("image") !== -1 ? file.target.files[0].name : '';
  }

  addEvent(event: MatDatepickerInputEvent<any>){
    this.job.finishedOn= event.value;
  }

}
