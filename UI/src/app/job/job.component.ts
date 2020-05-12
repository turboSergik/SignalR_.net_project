import {Component, OnInit} from '@angular/core';
import {JobService} from '../_services/job.service';
import {AuthService} from '../_services/auth.service';
import {ToolBarService} from '../_services/toolbar.service.service';
import {JobDTO} from '../_models/DTO/JobDTO';
import {PaginatedRequest} from '../_models/PaginatedRequest';
import {ActivatedRoute, Params} from '@angular/router';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {MatDialog} from '@angular/material/dialog';
import {ChatComponent} from '../chat/chat.component';


@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.css']
})

export class JobComponent implements OnInit {
  pageOptions = [1, 2, 3, 4, 5];
  jobsTypeId: number;
  filter = {} as PaginatedRequest;
  allJobs: JobDTO[];


  constructor(public dialog: MatDialog,
              private jobService: JobService,
              private toolBarService: ToolBarService,
              route: ActivatedRoute) {
    route.params.subscribe((params: Params) => {
      this.jobsTypeId = Number(params.id);
    });
  }

  ngOnInit() {
    this.toolBarService.setTitle('Jobs');
    this.filter.pageSize = 100;
    this.jobsTypeId ? this.loadJobsByTypeId(this.jobsTypeId) : this.loadJobs();
  }

  onFiltered(filter: PaginatedRequest) {
    this.filter = Object.assign(this.filter, filter);
    this.loadJobs();
  }

  loadJobsByTypeId(id: number) {
    this.jobService.getJobsByType(this.filter, id).subscribe( data => {
      this.allJobs = data.items;
    });
  }

  loadJobs() {
    this.jobService.getAllJobPaginated(this.filter).subscribe(data => {
      this.allJobs = data.items;
    });
  }

  /*
    if we click on chat button this func well be called
    jobId - job identifier
    we open new modal with chat component, provide jobId him
   */
  openChat(jobId: number): void {
    const dialogRef = this.dialog.open(ChatComponent, {
      width: '600px',
      height: '500px',
      data: jobId
    });
  }
}

