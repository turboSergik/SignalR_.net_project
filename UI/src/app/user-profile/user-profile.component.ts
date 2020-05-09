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


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})


export class UserProfileComponent implements OnInit {
  searchInput = new FormControl('');
  filter = {} as PaginatedRequest;
  displayedColumns: string[] = ['Id', 'Title', 'Category','City', 'Publish Date', 'Finish Date' , 'Contact','Actions'];

  dialogRef: MatDialogRef<any>;
  jobsCategories = [] as CategoryDTO[];
  jobsCities = [] as CityDTO[];
  jobTypes = [] as JobType[];
  employerJobs = [] as JobDTO[];


  paginationOptions = {
    pageSizeOptions: [1, 2, 3, 4],
    pageSize: 4,
    pageIndex: 0,
    length: 0
  };

  orderingOptions = {
    titleOrder: 'asc',
    categoryOrder: 'asc',
    cityOrder: "asc",
    publishedOrder : "asc",
    finishedOrder : "asc"
  };

  constructor(private jobService: JobService, public authService : AuthService,
               private toolBarService: ToolBarService,
               public dialog: MatDialog, private popService : PopService) { }

  ngOnInit() {
    this.filter.pageSize = this.paginationOptions.pageSize;
    this.toolBarService.setTitle('Profile');
    this.loadUserJobs();

    this.jobService.getCategories().subscribe(data => {
      this.jobsCategories = data;
    });

    this.jobService.getJobsTypes().subscribe((jobTypes: JobType[]) => {
      this.jobTypes = jobTypes;
    });

    this.jobService.getCity().subscribe(data => {
      this.jobsCities = data;
    });
    this.popService.onPopup().subscribe((id:number) =>{
      this.onDelete(id);
    });
  }

  loadUserJobs() {
    this.jobService.getAllJobPaginatedUser(this.filter).subscribe(data => {
      this.employerJobs = data.items;
      this.paginationOptions.length = data.total;
    });
  }

  onPaginatorChange(pagination) {

    this.filter = {
      ...this.filter,
      pageSize: pagination.pageSize,
      pageIndex: pagination.pageIndex
    };
    this.loadUserJobs();
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
      this.loadUserJobs();
    });
  }

   onDelete(id: number){
       this.jobService.deleteJob(id).subscribe(() => { this.loadUserJobs(); });
   }

  onClickPopUp(id:number, title: string){
     this.dialogRef = this.dialog.open(PopUpComponent, {
       data: {
         categories: this.jobsCategories,
         cities: this.jobsCities,
         id: id ? id : null,
         jobTitle : title
       }
     });
  }

 filterByAttribute = (columnNameForSorting, order) => {
    this.filter.columnNameForSorting = columnNameForSorting;
    this.orderingOptions[order] = this.orderingOptions[order] == 'asc' ? 'desc' : 'asc';
    this.filter.sortDirection = this.orderingOptions[order];

    this.jobService.getAllJobPaginatedUser(this.filter).subscribe(data => {
        this.employerJobs = data.items;
   });
  }

  createFilterFromSearchInput(){
    const filterValue = this.searchInput.value.trim();
    console.log(filterValue);
    if(filterValue){
      var filters: Filter[] = [];
      const filter: Filter = {value: filterValue, path : 'title'};
      filters.push(filter);

    }
    this.filter.requestFilters = {
      logicalOperator : FilterLogicalOperators.And,
      filters : filters
    };
    this.jobService.getAllJobPaginatedUser(this.filter).subscribe(data => {
      this.employerJobs = data.items;
    });
    console.log(this.filter);
  }

}
