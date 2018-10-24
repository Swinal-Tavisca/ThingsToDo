import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { Airport } from '../airport.service';
import { DataService } from '../dataService.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  value:any;
  response:any;
  panelColor = new FormControl('red');
  airportArea: string = 'InsideOutsideAirport';

  myControl = new FormControl();
  options: string[] = ['Bar', 'Spa', 'Store'];
  filteredOptions: Observable<string[]>;

  type:any;
  location:any;
  arrivalDatetime:any;
  DepartureDateTime:any;
  durationminutes:any;
  arrivalterminal:any;
  departureterminal:any;

  constructor(public airportServices: Airport,private route: ActivatedRoute,private http: HttpClient, public dataService: DataService) {}
  
  ngOnInit() {
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value))
    );
  }

  setAirportArea(area) {
    this.airportServices.setArea(area);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().indexOf(filterValue) === 0);
  }


  selected = 'outside';
  isExpanded = false;

  check() {
    this.airportServices.setInput(this.value);
     this.location = this.route.snapshot.queryParamMap.get('location');
     this.arrivalDatetime = this.route.snapshot.queryParamMap.get('ArrivalDateTime');
     this.DepartureDateTime = this.route.snapshot.queryParamMap.get('DepartureDateTime');
     this.arrivalterminal = this.route.snapshot.queryParamMap.get('ArrivalTerminal');
     this.departureterminal = this.route.snapshot.queryParamMap.get('DepartureTerminal');
     this.http.get('http://localhost:49542/api/Data/'+ this.airportServices.area +'/'+ this.location +'/' + this.arrivalDatetime +'/' +  this.DepartureDateTime +'/' + this.airportServices.getInput()).
   subscribe((response)=>
   {
     this.response=response;
     this.dataService.response=this.response;
   });
     
    
 
   }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  title = 'ClientApp things ';

}
