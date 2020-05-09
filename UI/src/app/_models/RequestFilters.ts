import { Filter } from './Filter';
import { FilterLogicalOperators } from './FilterLogicalOperators';

export interface RequestFilters{
    logicalOperator: FilterLogicalOperators;
    filters: Filter[];
}