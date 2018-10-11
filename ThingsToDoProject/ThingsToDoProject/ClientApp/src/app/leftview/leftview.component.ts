import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-leftview',
  templateUrl: './leftview.component.html',
  styleUrls: ['./leftview.component.css']
})
export class LeftviewComponent implements OnInit {

  constructor(private router: Router) { 
      console.log(this.router.url,"Current URL");
  }
  ngOnInit() {
  }

}
