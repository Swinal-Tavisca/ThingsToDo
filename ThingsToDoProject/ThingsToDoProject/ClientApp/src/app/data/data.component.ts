
import { Component, OnInit, Output, EventEmitter, HostListener } from '@angular/core';
import { DataService } from '../dataService.service';

@Component({
  selector: 'app-data',
  templateUrl: './data.component.html',
  styleUrls: ['./data.component.css']
})

export class DataComponent implements OnInit {
  @Output() toggle: EventEmitter<null> = new EventEmitter();
  @Output() info: EventEmitter<string> = new EventEmitter<string>();
  @HostListener('click')
  click() {
    this.toggle.emit();
  }

  constructor(public dataService: DataService) { }
  ngOnInit() { }
  counter(i: number) {
    var z;
    z = Math.floor(i);
    return new Array(z);
  }

  sideSectionIsOpened = false;
  MoreInfoOpen() {
    this.sideSectionIsOpened = !this.sideSectionIsOpened;
  }

  MoreInfo(placeid: string) {
    this.info.emit(placeid);
  }
}
