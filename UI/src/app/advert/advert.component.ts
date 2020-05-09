import {Component, Inject, OnInit} from '@angular/core';
import {AdvertDTO} from '../_models/DTO/AdvertDTO';
import {AdvertService} from '../_services/advert.service';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import {PopUpAdvertComponent} from './pop-up-advert/pop-up-advert.component';
import {UpdateAdvertComponent} from './update-advert/update-advert.component';
import {JobService} from '../_services/job.service';
import {CategoryDTO} from '../_models/DTO/CategoryDTO';
import {CityDTO} from '../_models/DTO/CityDTO';
import {FormControl} from '@angular/forms';
import {PaginatedRequest} from '../_models/PaginatedRequest';
import {log} from 'util';

@Component({
  selector: 'app-advert',
  templateUrl: './advert.component.html',
  styleUrls: ['./advert.component.css']
})
export class AdvertComponent implements OnInit {

  columnsToDisplay = ['Title','Category', 'City','Contact', 'PublishedOn','Description', 'Actions'];
  searchInput = new FormControl('');
  filter = {} as PaginatedRequest;
  constructor(private advertService: AdvertService,public dialog: MatDialog, private jobService: JobService) { }

  adverts = [] as AdvertDTO[];
  dialogRef: MatDialogRef<any>;
  Categories = [] as CategoryDTO[];
  Cities = [] as CityDTO[];


  ngOnInit(): void {

    this.jobService.getCategories().subscribe(data =>{this.Categories = data;});
    this.jobService.getCity().subscribe(data =>{this.Cities = data;});


        this.advertService.onPopup().subscribe((id:number) =>{
          this.onDelete(id);
        });
          // ascult evenimentul ???
        this.advertService.onAddAdvert().subscribe(() => {this.loadAdverts();});
        this.loadAdverts();
  }

  loadAdverts(){
    this.advertService.getAdverts().subscribe(data => {this.adverts = data});
  }

  onDelete(id:number){
    this.advertService.deleteAdvert(id).subscribe();
  }

  openPopUp(id :number, AddName : string){
    this.dialogRef = this.dialog.open(PopUpAdvertComponent, {
      data: {
        id: id ? id : null,
        name : AddName
      }
    });
  }

  onUpdate(id: number) {
    this.dialogRef = this.dialog.open(UpdateAdvertComponent, {
      data: {
        id: id ? id : null,
        categories : this.Categories,
        cities: this.Cities
      },

    });
    console.log(this.adverts);
  }
}
