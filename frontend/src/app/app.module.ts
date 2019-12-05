import {Directive, HostListener, Injector} from '@angular/core';

@Directive({selector: '[clickCatcher]'})
export class ClickCatcher extends AppComponentBase {
  constructor(
    injector: Injector) {
    super(injector);
  }

  @HostListener('click', ['$event.target.id']) onClick(id: any) {
    // alert(`You clicked on ${id}`);

    if (id != null) {
      console.log(id);
      this.apiService.locationSource.next(id);
    }

  }


}


import {MapComponent} from './page/map/map.component';
import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';

import {LeafletModule} from '@asymmetrik/ngx-leaflet';
import {ModalModule} from 'ngx-bootstrap';
import {FormsModule} from '@angular/forms';
import {CoreModule} from './page/core/core.module';
import {HttpClientModule} from '@angular/common/http';
import {ShowLocationComponent} from './page/map/show-location/show-location.component';
import {AppComponentBase} from "./page/core/base/app-base.component";

@NgModule({
  declarations: [
    AppComponent,
    MapComponent,
    ClickCatcher,
    ShowLocationComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    CoreModule,
    LeafletModule.forRoot(),
    ModalModule.forRoot(),
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}


