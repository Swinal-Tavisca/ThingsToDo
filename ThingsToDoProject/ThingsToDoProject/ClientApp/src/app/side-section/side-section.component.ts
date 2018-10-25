import { Component, OnInit, HostBinding, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-side-section',
  templateUrl: './side-section.component.html',
  styleUrls: ['./side-section.component.css']
})
export class SideSectionComponent implements OnInit {
  @Input() PlaceId: string=null;
  location:string;
  response: any;

  
  constructor(private route: ActivatedRoute, private http: HttpClient) { 
    this.location = this.route.snapshot.queryParamMap.get('location');
  }

  SetReminder(){
    this.http.get('http://localhost:50298/api/Data/reminder/' + this.response.phoneNumber + '/')
    .subscribe(
      error => console.log("Error with Twillio",error),
    );
  }

  ngOnChanges(){
  if(this.PlaceId!=null){
    this.GetAllDataOfParticularPlace();
    this.PlaceId=null;
  }
}

GetAllDataOfParticularPlace(){
  this.http.get('http://localhost:50298/api/Data/place/'+ this.location + '/'+this.PlaceId )
  .subscribe(
    data => this.response=data,
    error => this.response=false,
  );
  }

  // subscribe((response)=>
  // {
  //   this.response = response;
  //   if(this.response==null){
  //     this.response=null;
  //   }
  // })

  ngOnInit() {
  }
  @HostBinding('class.is-open') @Input()
  isOpen = false;

}
