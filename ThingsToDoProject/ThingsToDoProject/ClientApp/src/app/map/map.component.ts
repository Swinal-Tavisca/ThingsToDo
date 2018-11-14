import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Airport } from '../airport.service';
import { DataService } from '../dataService.service';
import { Observable, observable } from 'rxjs';


@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  errorMsg: any;
  Getresponse: any;
  res: any;
  zoom: number = 15;
  lat: number;
  lng: number;
  isDataLoaded: boolean = false;
  iconUrl: string = "../src/assets/images/icons8-user-location-48.png";
  public destination = this.dataService.destination;
  public origin: {
    lat: number,
    lng: number
  };
  city: string;
  image: string = "../src/assets/images/404notfound.jpg";
  loader: boolean;
  direction: boolean;
  data: any;
  getDataOfParticularPlace(marker: any, placeid: string) {
    this.loader = true;
    let observable = this.http.get('api/Data/place/' + this.location + '/' + placeid);
    observable.subscribe((response) => {
      this.res = response;
      if (this.res.image != null) {
        marker.image = this.res.image;
        marker.duration = this.res.duration;
        marker.distance = this.res.distance;
      }
      else {
        marker.image = this.image;
      }
      this.loader = false;
    },
      error => {

        if (error.status == 400 || error.status == 404 || error.status == 403) {
          marker.image = this.image;
        }
        this.loader = false;
      })
  }

  public renderOptions = {
    suppressMarkers: true,
    polylineOptions: {
      strokeColor: '#00F',
      strokeOpacity: 0.6,
      strokeWeight: 5,
    }
  }

  type: any;
  location: any;
  arrivalDatetime: any;
  DepartureDateTime: any;
  durationminutes: any;
  arrivalterminal: any;
  departureterminal: any;

  constructor(public airportServices: Airport, private route: ActivatedRoute, private router: Router, private http: HttpClient, public dataService: DataService) {
    dataService.direction = false;
    this.airportServices.setInput(this.router.url.substring(1, this.router.url.indexOf('?')));
    this.location = this.route.snapshot.queryParamMap.get('location');
    this.arrivalDatetime = this.route.snapshot.queryParamMap.get('ArrivalDateTime');
    this.DepartureDateTime = this.route.snapshot.queryParamMap.get('DepartureDateTime');
    this.arrivalterminal = this.route.snapshot.queryParamMap.get('ArrivalTerminal');
    this.departureterminal = this.route.snapshot.queryParamMap.get('DepartureTerminal');
    this.durationminutes = this.route.snapshot.queryParamMap.get('DurationMinutes');
  }
  markers: Array<marker> = [];
  response: any;

  ngOnInit() {
    this.loader = false
    this.isDataLoaded = true;
    this.city = this.route.snapshot.queryParamMap.get('location');

    this.http.get('api/Data/position/' + this.city).subscribe((response) => {
      this.Getresponse = response;
      this.lat = this.Getresponse.latitudePosition;
      this.lng = this.Getresponse.longitudePosition;
      this.origin = { lat: this.lat, lng: this.lng };
      this.dataService.origin = this.origin;
    })
    this.http.get('api/Data/' + this.airportServices.area + '/' + this.location + '/' + this.arrivalDatetime + '/' + this.DepartureDateTime + '/' + this.airportServices.getInput() + '/' + this.durationminutes).
      subscribe((response) => {
        // console.log(response);
        this.response = response;
        for (let data in response) {
          this.markers.push({
            lat: Number(response[data].latitude),
            lng: Number(response[data].longitude),
            name: response[data].name,
            rating: response[data].rating,
            placeID: response[data].placeID,
            image: "",
            duration: "",
            distance: ""
          })
          this.dataService.response = this.response;
        }
      })
  }
}

class marker {
  lat: number;
  lng: number;
  name: string;
  rating: string
  placeID: string;
  image: string;
  distance: string;
  duration: string;
}
