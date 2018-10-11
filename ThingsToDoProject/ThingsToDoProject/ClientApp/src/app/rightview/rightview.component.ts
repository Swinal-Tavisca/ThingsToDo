import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-rightview',
  templateUrl: './rightview.component.html',
  styleUrls: ['./rightview.component.css']
})
export class RightviewComponent implements OnInit {

  constructor(private router: Router) { 
    console.log(this.router.url,"Current Map URL");
}

  ngOnInit() {
  }

}
