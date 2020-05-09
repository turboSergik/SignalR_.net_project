import {CategoryDTO} from './DTO/CategoryDTO';
import {CityDTO} from './DTO/CityDTO';
import {JobType} from './JobType';

export interface AddJobData {
  categories: CategoryDTO[];
  jobTypes: JobType[];
  cities: CityDTO[];
  id?: number;
  jobTitle : string;
}
