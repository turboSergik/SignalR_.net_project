import {Component, EventEmitter, Output} from '@angular/core';
import {PaginatedRequest} from '../../_models/PaginatedRequest';
import {FormControl} from '@angular/forms';
import {FilterLogicalOperators} from '../../_models/FilterLogicalOperators';
import {Filter} from '../../_models/Filter';

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
  searchInput = new FormControl('');
  sortFilters = [] as Filter[];
  @Output() onFiltered = new EventEmitter<PaginatedRequest>();
  @Output() onSearchChange = new EventEmitter<Object>();

  filterByAttribute = (columnNameForSorting, order) => {
    this.filter.columnNameForSorting = columnNameForSorting;
    this.orderingOptions[order] = this.orderingOptions[order] == 'asc' ? 'desc' : 'asc';
    this.filter.sortDirection = this.orderingOptions[order];
    this.onFiltered.emit(this.filter);
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

    this.onSearchChange.emit(this.filter.requestFilters);
  }
}
