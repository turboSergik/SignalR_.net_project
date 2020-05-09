import {AfterContentInit, Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {PaginatedRequest} from '../../_models/PaginatedRequest';

@Component({
  selector: 'app-job-filter',
  templateUrl: './job-filter.component.html',
  styleUrls: ['./job-filter.component.css']
})
export class JobFilterComponent {
  orderingOptions = {
    titleOrder: 'asc',
    categoryOrder: 'asc'
  };

  filter = {} as PaginatedRequest;
  @Output()
  onFiltered = new EventEmitter<PaginatedRequest>();

  constructor() {
  }

  filterByAttribute = (columnNameForSorting, order) => {
    this.filter.columnNameForSorting = columnNameForSorting;
    this.orderingOptions[order] = this.orderingOptions[order] == 'asc' ? 'desc' : 'asc';
    this.filter.sortDirection = this.orderingOptions[order];
    this.onFiltered.emit(this.filter);
  };
}
