import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.css']
})
export class MainContentComponent implements OnInit {
  PlaceId: string;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.sideSectionIsOpened = false;
  }
  sideSectionIsOpened = false;

  toggleSideSection() {
    this.sideSectionIsOpened = !this.sideSectionIsOpened;
  }

  MoreInfo(Id: string){
    this.PlaceId = Id;
    this.sideSectionIsOpened = !this.sideSectionIsOpened;
  }
}
