import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {AdvertData} from '../../_models/AdvertData';
import {CategoryDTO} from '../../_models/DTO/CategoryDTO';
import {CityDTO} from '../../_models/DTO/CityDTO';
import {AdvertDTO} from '../../_models/DTO/AdvertDTO';
import {AdvertService} from '../../_services/advert.service';
import {JobService} from '../../_services/job.service';
import {JobDTO} from '../../_models/DTO/JobDTO';

@Component({
  selector: 'app-update-advert',
  templateUrl: './update-advert.component.html',
  styleUrls: ['./update-advert.component.css']
})
export class UpdateAdvertComponent implements OnInit {

  categories: CategoryDTO[];
  cities: CityDTO[];
  advert = {} as AdvertDTO;

  constructor(@Inject(MAT_DIALOG_DATA) public advertData: AdvertData,
              public dialogRef: MatDialogRef<UpdateAdvertComponent>,
              private advertService : AdvertService,
              public  jobService : JobService){}

  ngOnInit(): void {
    this.jobService.getCategories().subscribe(data => {this.categories = data});
    this.jobService.getCity().subscribe(data =>{this.cities = data});

    if (this.advertData.id) {
      this.advertService.getPostedAdvert(this.advertData.id).subscribe((advert: AdvertDTO) => {
        this.advert = advert;
      });
    }
  }

  AddAdvert(){
    this.advertService.postStudentAdverts(this.advert).subscribe(() => {
      this.advertService.popUp.next();
      this.dialogRef.close();
    });
  }

  UpdateAdvert(id:number){
      this.advertService.putAdvert(this.advert, id).subscribe(()=> {
        this.advertService.popUp.next();
        this.dialogRef.close();
      });
  }
}
