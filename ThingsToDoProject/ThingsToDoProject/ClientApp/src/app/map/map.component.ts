import { Component, OnInit,Output, EventEmitter, HostListener } from '@angular/core';
// import { SideBarComponent } from '../side-bar/side-bar.component';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit{
  @Output() toggle: EventEmitter<null> = new EventEmitter();

  @HostListener('click')
  click() {
    this.toggle.emit();
  }

  Getresponse:any;

  zoom :number = 15;
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
type:any;
  location:any;
  arrivalDatetime:any;
  DepartureDateTime:any;
  durationminutes:any;
  arrivalterminal:any;
  departureterminal:any;

constructor(private route: ActivatedRoute, private router: Router , private http: HttpClient) { 
  console.log(this.router.url,"Current URL");
  this.type= this.router.url.substring(1,this.router.url.indexOf('?'));
  this.location = this.route.snapshot.queryParamMap.get('location');
  this.arrivalDatetime = this.route.snapshot.queryParamMap.get('ArrivalDateTime');
  this.DepartureDateTime = this.route.snapshot.queryParamMap.get('DepartureDateTime');
  this.arrivalterminal = this.route.snapshot.queryParamMap.get('ArrivalTerminal');
  this.departureterminal = this.route.snapshot.queryParamMap.get('DepartureTerminal');
  console.log(this.durationminutes = this.route.snapshot.queryParamMap.get('DurationMinutes'));
  console.log(this.arrivalterminal);
  console.log(this.departureterminal);
}
markers: Array<marker>=[];
response: any;
 
  ngOnInit() {
    
    this.isDataLoaded=true;
    this.city= this.route.snapshot.queryParamMap.get('location');
    console.log(this.city);
    this.http.get('http://localhost:57985/api/Data/position/'+this.city).subscribe((response)=>{
      this.Getresponse = response;
      this.lat =  this.Getresponse.latitudePosition;
      this.lng=this.Getresponse.longitudePosition;
      
    })

this.http.get('http://localhost:57985/api/Data/outsideAirport/'+ this.location +'/' + this.arrivalDatetime +'/' +  this.DepartureDateTime +'/' + this.type).
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

