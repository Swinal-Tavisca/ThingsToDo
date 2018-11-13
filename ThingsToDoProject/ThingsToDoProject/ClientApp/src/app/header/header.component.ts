import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Airport } from '../airport.service';
import { DataService } from '../dataService.service';
import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

export interface DialogData {
  phonenumber: number;
}

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  phonenumber: number;
  value: any;
  response: any;
  panelColor = new FormControl('red');
  airportArea: string = 'InsideOutsideAirport';

  type: any;
  location: any;
  arrivalDatetime: any;
  DepartureDateTime: any;
  durationminutes: any;
  arrivalterminal: any;
  departureterminal: any;
  hours: any;
  minutes: any;
  duration: any;
  url: any;

  constructor(public airportServices: Airport, private route: ActivatedRoute, private router: Router, private http: HttpClient, public dataService: DataService, public dialog: MatDialog) {
    setTimeout(() => {
      this.location = this.route.snapshot.queryParams['location'];
      this.durationminutes = this.route.snapshot.queryParamMap.get('DurationMinutes');
      this.getTime(this.durationminutes);
    }, 1000);
  }

  getTime(durationminutes) {
    this.hours = Math.floor(durationminutes / 60);
    this.minutes = Math.floor(durationminutes % 60);
    this.duration = this.hours + "Hour" + " " + this.minutes + "Minutes";
  }

  ngOnInit() {
  }
  SetReminder() {
    this.http.get('api/Data/reminder/' + this.phonenumber + "?returnUrl" + this.url)
      .subscribe();

  }
  openDialog(): void {
    const dialogRef = this.dialog.open(DialogOverview, {
      width: '250px',
      data: { phonenumber: this.phonenumber }

    });

    // dialogRef.afterClosed().subscribe(result => {
    //   console.log('The dialog was closed');
    //   this.phonenumber = result;
    //   console.log(this.phonenumber);
    //   //window.location.href="https://wa.me/14155238886?text=join%20lemon-mule";
    //   // this.SetReminder();
    // });
  }

  setAirportArea(area) {
    this.dataService.direction = false
    this.airportServices.setArea(area);
    this.airportArea = area;
  }

  selected = 'outside';
  isExpanded = false;
  eventHandler(keyCode) {

    if (keyCode == 13)
      this.check();
  }

  check() {
    this.airportServices.setInput(this.value);
    this.location = this.route.snapshot.queryParamMap.get('location');
    this.arrivalDatetime = this.route.snapshot.queryParamMap.get('ArrivalDateTime');
    this.DepartureDateTime = this.route.snapshot.queryParamMap.get('DepartureDateTime');
    this.arrivalterminal = this.route.snapshot.queryParamMap.get('ArrivalTerminal');
    this.departureterminal = this.route.snapshot.queryParamMap.get('DepartureTerminal');
    this.durationminutes = this.route.snapshot.queryParamMap.get('DurationMinutes');
    this.http.get('api/Data/search/' + this.location + ' / ' + this.arrivalDatetime + ' / ' + this.DepartureDateTime + ' / ' + this.airportServices.getInput() + '/' + this.durationminutes + '/' + this.airportArea).
      subscribe((response) => {
        this.response = response;
        this.dataService.response = this.response;
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

@Component({
  selector: 'DialogOverview',
  templateUrl: 'DialogOverview.html',
})
export class DialogOverview {

  constructor(
    public dialogRef: MatDialogRef<DialogOverview>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {

  }

  onNoClick(): void {
    this.dialogRef.close();
  }
  onClickAuthorize(): void {
    window.open(
      'https://wa.me/14155238886?text=join%20lemon-mule',
      '_blank'
    );


  }

}
