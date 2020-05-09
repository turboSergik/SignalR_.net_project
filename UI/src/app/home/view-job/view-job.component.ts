import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import {JobService} from '../../_services/job.service';
import {JobDTO} from '../../_models/DTO/JobDTO';

@Component({
  selector: 'app-view-job',
  templateUrl: './view-job.component.html',
  styleUrls: ['./view-job.component.css']
})
export class ViewJobComponent implements OnInit {
  job: JobDTO;

  constructor(@Inject(MAT_DIALOG_DATA) public jobData: {id: number},
              private jobService: JobService) { }

  ngOnInit(): void {
    this.jobService.getJobById(this.jobData.id).subscribe((job:JobDTO)=> {
      this.job = job;
    });
  }

}
