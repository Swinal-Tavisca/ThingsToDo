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
     this.http.get('http://localhost:51346/api/InsideAirport').
    subscribe((response)=>
    {
    this.response = response;
    
    console.log(this.response);
    
    console.log(this.response[0]);
    
    for(let data in response){
    
    this.markers.push({
    
    lat: Number(response[data].longitute),
    
    lng: Number(response[data].latitude)
    
    })
    
    console.log(typeof this.markers[data].lat);
    
    }
    
    console.log(this.markers);
    
    for(let m of this.markers) {
    
    console.log(m.lat);
    
    }
    console.log(this.route.snapshot.queryParamMap.has('location'));
    console.log(this.route.snapshot.queryParamMap.get('location'));
    console.log(this.route.snapshot.queryParamMap.has('time'));
    console.log(this.route.snapshot.queryParamMap.get('time'));
    })
  }
    
  




  lat: number = 18.573013;
  lng: number = 73.907546;
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
    lat:18.573013,
    lng: 73.907546,
  
  },
  {
    lat:18.573013,
    lng: 73.907546,
 
  },
  {
    lat:18.573013,
    lng: 73.907546,
  }
]
}
interface marker {
lat: number;
lng: number;

}

