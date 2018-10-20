import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-data',
  templateUrl: './data.component.html',
  styleUrls: ['./data.component.css']
})
export class DataComponent implements OnInit {
  response: any;
  type:any;
  location:any;
  arrivalDatetime:any;
  DepartureDateTime:any;
  durationminutes:any;
  arrivalterminal:any;
  departureterminal:any;

  constructor(private route: ActivatedRoute, private router: Router , private http: HttpClient) { 
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

  ngOnInit() {
    this.http.get('http://localhost:53617/api/Data/insideAirport/'+ this.location +'/' + this.arrivalDatetime +'/' +  this.DepartureDateTime +'/' + this.type).
    subscribe((response)=>
    {
    this.response = response;
    })

  }
}