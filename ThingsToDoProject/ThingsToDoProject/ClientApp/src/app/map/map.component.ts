import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  public response:any;
  _i:any;
  constructor(private route: ActivatedRoute, private router: Router , private http: HttpClient) { 
    console.log(this.router.url,"Current URL");
    
}

  ngOnInit() {
    this.http.get('http://localhost:55600/api/InsideAirport').
    subscribe((response)=>
    {
    this.response = response;
    for (this._i= 0; this._i < this.response.length; this._i++) {
      var num = this.response[this._i];
      console.log(num);
    }
    console.log(this.response);
    })
    console.log(this.route.snapshot.queryParamMap.has('location'));
    console.log(this.route.snapshot.queryParamMap.get('location'));
    console.log(this.route.snapshot.queryParamMap.has('time'));
    console.log(this.route.snapshot.queryParamMap.get('time'));
  }

  lat: number =51.373858;
  lng: number = 5.809007;
  public origin: any;
  public destination: any;
  

   
  getDirection(marker,latitude: number,longitude: number) {
    this.origin = { lat: 51.373858, lng: 5.809007 }
    this.destination = { lat: latitude, lng: longitude}
    console.log('xyz');
    
//marker.iconUrl = "http://maps.google.com/mapfiles/ms/icons/red-dot.png";
  }
  public renderOptions = {
    suppressMarkers: true,
}


markers: marker[] = [
  {
    lat:51.373858,
    lng: 5.809007,
    label: '',
   name:"KFc",
   rating:"****",
   iconUrl:"assets/images/icons8-user-location-48.png",
   markerClickable:false,
  
  },
  {
    lat: 51.373858,
    lng: 7.215982,
    label: 'B',
    markerClickable:true,
  
    name:"subway",
    rating:"****",
    iconUrl:"",
 
  },
  {
    lat: 51.723858,
    lng: 7.895982,
    label: 'C',
    markerClickable:true,
    
    name:"mac-donalds",
    rating:"****",

    iconUrl:"",

  }
]
}
interface marker {
lat: number;
lng: number;
label?: string;
markerClickable:boolean;

name:string,
rating:string,
iconUrl:string,

}

