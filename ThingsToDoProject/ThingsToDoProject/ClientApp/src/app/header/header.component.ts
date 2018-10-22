import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { Airport } from '../airport.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  value:any;
  panelColor = new FormControl('red');
  airportArea: string = 'InsideOutsideAirport';

  myControl = new FormControl();
  options: string[] = ['Bar', 'Spa', 'Store'];
  filteredOptions: Observable<string[]>;

  constructor(public airportServices: Airport) {}
  
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

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  title = 'ClientApp things ';

}
