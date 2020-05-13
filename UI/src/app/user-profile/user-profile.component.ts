import {Component, OnInit} from '@angular/core';
import {JobService} from '../_services/job.service';
import {ToolBarService} from '../_services/toolbar.service.service';
import {UpdateJobComponent} from './update-job/update-job.component';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {PaginatedRequest} from '../_models/PaginatedRequest';
import {CategoryDTO} from '../_models/DTO/CategoryDTO';
import {CityDTO} from '../_models/DTO/CityDTO';
import {JobDTO} from '../_models/DTO/JobDTO';
import {PopService} from '../_services/pop.service';
import {PopUpComponent} from './pop-up/pop-up.component';
import {AuthService} from '../_services/auth.service';
import {FormControl} from '@angular/forms';
import {Filter} from '../_models/Filter';
import {FilterLogicalOperators} from '../_models/FilterLogicalOperators';
import {JobType} from '../_models/JobType';
import {NgxPermissionsService} from 'ngx-permissions';
import {AdvertService} from '../_services/advert.service';


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})


export class UserProfileComponent implements OnInit {
  searchInput = new FormControl('');
  filter = {} as PaginatedRequest;
  postedJobsColumns: string[] = ['Id', 'Title', 'Category', 'City', 'Publish Date', 'Finish Date', 'Contact', 'Actions'];
  enrolledJobsColumns = ['Title', 'Category', 'City', 'Contact', 'PublishedOn', 'Description', 'Actions'];

  dialogRef: MatDialogRef<any>;
  jobsCategories = [] as CategoryDTO[];
  jobsCities = [] as CityDTO[];
  jobTypes = [] as JobType[];
  employerJobs = [] as JobDTO[];
  studentEnrolledJobs = [] as JobDTO[];
  sortFilters = [] as Filter[];


  paginationOptions = {
    pageSizeOptions: [1, 2, 3, 4],
    pageSize: 4,
    pageIndex: 0,
    length: 0
  };

  orderingOptions = {
    titleOrder: 'asc',
    categoryOrder: 'asc',
    cityOrder: 'asc',
    publishedOrder: 'asc',
    finishedOrder: 'asc'
  };

  constructor(private jobService: JobService,
              private toolBarService: ToolBarService,
              private permissionsService: NgxPermissionsService,
              public dialog: MatDialog, private popService: PopService) {
  }

  ngOnInit() {
    this.filter.pageSize = this.paginationOptions.pageSize;
    this.toolBarService.setTitle('Profile');
    this.permissionsService.permissions$
      .subscribe((permissions: any) => {
        switch (Object.keys(permissions)[0]) {
          case 'Student':
            this.loadStudentJobs();
            break;
          case 'Employer':
            this.loadEmployerJobs();
            break;
        }
      });

    this.jobService.getCategories().subscribe(data => {
      this.jobsCategories = data;
    });

    this.jobService.getJobsTypes().subscribe((jobTypes: JobType[]) => {
      this.jobTypes = jobTypes;
    });

    this.jobService.getCity().subscribe(data => {
      this.jobsCities = data;
    });
    this.popService.onPopup().subscribe((id: number) => {
      this.onDelete(id);
    });
  }

  loadEmployerJobs() {
    this.jobService.getAllJobPaginatedEmployer(this.filter).subscribe(data => {
      this.employerJobs = data.items;
      this.paginationOptions.length = data.total;
    });
  }


  loadStudentJobs() {
    this.jobService.getAllJobPaginatedStudent(this.filter).subscribe(data => {
      this.studentEnrolledJobs = data.items;
      this.paginationOptions.length = data.total;
    });
  }

  onPaginatorChange(pagination) {
    this.filter = {
      ...this.filter,
      pageSize: pagination.pageSize,
      pageIndex: pagination.pageIndex
    };
    this.loadEmployerJobs();
  }

  onUpdate(id?: number) {
    this.dialogRef = this.dialog.open(UpdateJobComponent, {
      data: {
        categories: this.jobsCategories,
        cities: this.jobsCities,
        jobTypes: this.jobTypes,
        id: id ? id : null
      }
    });

    this.dialogRef.afterClosed().subscribe(() => {
      this.loadEmployerJobs();
    });
  }

  onDelete(id: number) {
    this.jobService.deleteJob(id).subscribe(() => {
      this.loadEmployerJobs();
    });
    this.jobService.deleteEnrolledJobForStudent(id).subscribe( () => {this.loadStudentJobs(); console.log(id)});
  }

  onClickPopUp(id: number, title: string) {
    this.dialogRef = this.dialog.open(PopUpComponent, {
      data: {
        categories: this.jobsCategories,
        cities: this.jobsCities,
        id: id ? id : null,
        jobTitle: title
      }
    });
  }

  filterByAttribute = (columnNameForSorting, order) => {
    this.filter.columnNameForSorting = columnNameForSorting;
    this.orderingOptions[order] = this.orderingOptions[order] == 'asc' ? 'desc' : 'asc';
    this.filter.sortDirection = this.orderingOptions[order];
    this.loadEmployerJobs();
  };

  onSearch() {
    const filterValue = this.searchInput.value.trim();
    if (filterValue) {
      this.sortFilters = [
        {
          value: filterValue,
          path: 'title'
        }
      ];
    }
    this.filter.requestFilters = {
      logicalOperator: FilterLogicalOperators.And,
      filters: this.sortFilters
    };
    this.loadEmployerJobs();
    this.sortFilters = [];
  }
}
