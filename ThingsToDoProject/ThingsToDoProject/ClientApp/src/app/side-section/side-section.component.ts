import { Component, OnInit, HostBinding, Input } from '@angular/core';

@Component({
  selector: 'app-side-section',
  templateUrl: './side-section.component.html',
  styleUrls: ['./side-section.component.css']
})
export class SideSectionComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  @HostBinding('class.is-open') @Input()
  isOpen = false;
}
