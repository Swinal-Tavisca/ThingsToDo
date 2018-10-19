import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit{
  title: string = 'My first AGM project';
  Getresponse:any;
  lat: number ;
  lng: number ;
  isDataLoaded:boolean = false;
  iconUrl: string = "assets/images/icons8-user-location-48.png";
  public origin: any; 
  public destination: any;
 city:string;
  getDirection(latitude: number,longitude: number) {
    console.log(this.Getresponse);
    console.log(this.Getresponse.latitudePosition);
    this.origin = { lat:this.Getresponse.latitudePosition, lng:this.Getresponse.longitudePosition};
    this.destination = { lat: latitude, lng: longitude};
  }
  closeInfoWindow(infoWindow,gm)
  {
    console.log('amakh');
    if (gm.lastOpen != null) {
      gm.lastOpen.close();
  }

  }
  onMouseOver(infoWindow, gm) {

    if (gm.lastOpen != null) {
        gm.lastOpen.close();
    }

    gm.lastOpen = infoWindow;

    infoWindow.open();
}

  public renderOptions = {
    suppressMarkers: true,
}
constructor(private route: ActivatedRoute, private router: Router , private http: HttpClient) { 
  console.log(this.router.url,"Current URL");
  //console.log(this.city= this.route.snapshot.queryParamMap.get('location'));
}
markers: Array<marker>=[];
response: any;
  ngOnInit() {
    
    this.isDataLoaded=true;
    this.city= this.route.snapshot.queryParamMap.get('location');
    this.http.get('http://localhost:64570/api/Data/position/'+this.city).subscribe((response)=>{
      this.Getresponse = response;
      this.lat =  this.Getresponse.latitudePosition;
      this.lng=this.Getresponse.longitudePosition;
      
    })

 this.http.get('http://localhost:64570/api/Data/insideAirport/puneairport/12/13/store').
  subscribe((response)=>
  {
  this.response = response;
  for(let data in response){
    this.markers.push({
      lat: Number(response[data].latitude),
      lng: Number(response[data].longitude),
      name:response[data].name,
      rating:response[data].rating,

    })
  }
})
}
}
class marker {
  lat: number;
  lng: number;  
name:string;
rating:string

}