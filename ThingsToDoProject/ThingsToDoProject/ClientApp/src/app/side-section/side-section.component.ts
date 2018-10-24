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



  ngOnChanges(){
  if(this.PlaceId!=null){
    this.GetAllDataOfParticularPlace();
  }
}

GetAllDataOfParticularPlace(){
  this.http.get('http://localhost:49542/api/Data/place/'+ this.location + '/'+this.PlaceId ).
  subscribe((response)=>
  {
    this.response = response;
  })
}
  ngOnInit() {
  }
  @HostBinding('class.is-open') @Input()
  isOpen = false;
}
