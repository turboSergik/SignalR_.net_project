import { RequestFilters } from './RequestFilters';
import { MatSidenavContainer } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import {MatPaginator} from '@angular/material/paginator';



export class PaginatedRequest{
    pageIndex : number;
    pageSize:number;
    columnNameForSorting: string;
    sortDirection :string;
    requestFilters: RequestFilters;

    constructor(paginator: MatPaginator, sort: MatSort, filters: RequestFilters) {
        this.pageIndex = paginator.pageIndex;
        this.pageSize = paginator.pageSize;
        this.columnNameForSorting = sort.active;
        this.sortDirection = sort.direction;
        this.requestFilters = filters;
    }
}