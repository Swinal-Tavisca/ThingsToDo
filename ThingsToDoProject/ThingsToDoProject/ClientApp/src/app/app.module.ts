import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatChipsModule,MatTabsModule,MatSelectModule,MatFormFieldModule,MatAutocompleteModule,MatToolbarModule, MatSidenavModule, MatListModule, MatButtonModule, MatIconModule , MatInputModule,MatCardModule} from "@angular/material";
import { FlexLayoutModule } from "@angular/flex-layout";
import { AgmCoreModule} from '@agm/core';
import { AgmDirectionModule } from 'agm-direction';
import { AgmSnazzyInfoWindowModule } from '@agm/snazzy-info-window';
import { Airport } from './airport.service';

import 'hammerjs';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { DataComponent } from './data/data.component';
import { MainContentComponent } from './main-content/main-content.component';
import { MapComponent } from './map/map.component';
import { HttpClientModule } from '@angular/common/http';
import { ReminderComponent } from './reminder/reminder.component';
import { FormsModule,ReactiveFormsModule  } from '@angular/forms';
import { SideSectionComponent } from './side-section/side-section.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    DataComponent,
    MainContentComponent,
    MapComponent,
    ReminderComponent,
    SideSectionComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    MatTabsModule,
    MatChipsModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    FlexLayoutModule,
    MatInputModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatSelectModule,
    AgmCoreModule.forRoot(
      {
        apiKey:'AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A'
      }
    ), AgmDirectionModule,
    MatCardModule,
    AgmSnazzyInfoWindowModule,
    [BrowserAnimationsModule],

    RouterModule.forRoot([
      { path: '',redirectTo: '/Home', pathMatch: 'full' },
      { path: 'Home',component: MainContentComponent},
      { path: 'restaurant',component: MainContentComponent},
      { path: 'store',component: MainContentComponent},
      {path:'attractions',component:MainContentComponent},
      { path: '**',redirectTo: '/Home'},
      //{path:'attractions/:selected',component:MainContentComponent},
      
    ])
  ],
  providers: [ Airport ],
  bootstrap: [AppComponent]
})
export class AppModule { }
