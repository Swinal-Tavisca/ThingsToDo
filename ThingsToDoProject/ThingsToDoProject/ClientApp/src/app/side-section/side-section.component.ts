import { Component, OnInit, HostBinding, Input, Output, EventEmitter, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-side-section',
  templateUrl: './side-section.component.html',
  styleUrls: ['./side-section.component.css']
})
export class SideSectionComponent implements OnInit {
  @Input() PlaceId: string = null;
  location: string;
  response: any;
  image: string = "../src/assets/images/404notfound.jpg";
  loader: boolean;
  value: any;
  url: any;
  constructor(private route: ActivatedRoute, private http: HttpClient) {
    this.location = this.route.snapshot.queryParamMap.get('location');
  }
  @Output() toggle: EventEmitter<string> = new EventEmitter<string>();

  SetReminder() {
    this.url = window.location.href;
    this.http.get('api/Data/reminder/' + this.value + '/' + this.response.placeID + '/' + this.response.name + '/' + this.response.distance + '/' + this.response.phoneNumber + "?returnUrl=" + this.url)
      .subscribe();
    this.toggle.emit("ok");
  }

  ngOnChanges() {
    if (this.PlaceId != null) {
      this.GetAllDataOfParticularPlace();
      this.PlaceId = null;
    }
  }
  CloseSection() {
    this.toggle.emit("ok");
  }

  GetAllDataOfParticularPlace() {
    this.loader = true;
    let observable = this.http.get('api/Data/place/' + this.location + '/' + this.PlaceId)

    observable.subscribe((response) => {
      this.response = response;
      this.loader = false;
    },
      error => {
        if (error.status == 400 || error.status == 404) {
          this.response.image = this.image;
        }
        this.loader = false;
      })
  }

  ngOnInit() {
  }
  @HostBinding('class.is-open') @Input()
  isOpen = false;

}
