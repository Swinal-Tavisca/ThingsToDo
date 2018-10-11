import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule, MatSidenavModule, MatListModule, MatButtonModule, MatIconModule } from "@angular/material";
import { FlexLayoutModule } from "@angular/flex-layout";


import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { LeftviewComponent } from './leftview/leftview.component';
import { RightviewComponent } from './rightview/rightview.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LeftviewComponent,
    RightviewComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,
    FlexLayoutModule,
    RouterModule.forRoot([
      { path: '',component: LeftviewComponent, pathMatch: 'full' },
      { path: 'All',component: LeftviewComponent},
      { path: '**',component: RightviewComponent},
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
