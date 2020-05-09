import {CategoryDTO} from './DTO/CategoryDTO';
import {CityDTO} from './DTO/CityDTO';
import {AdvertDTO} from './DTO/AdvertDTO';

export interface AdvertData {
    id:number;
    advert : AdvertDTO;
    categories : CategoryDTO[];
    cities : CityDTO[];
}



