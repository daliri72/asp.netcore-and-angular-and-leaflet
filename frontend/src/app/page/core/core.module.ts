import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {Api2Service} from './services/api2.service';
import {APP_CONFIG, AppConfig} from './conf/app.config';
import {SweetalertService} from "./services/sweetalert.service";


@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    Api2Service,
    SweetalertService,
    {provide: APP_CONFIG, useValue: AppConfig}]
})
export class CoreModule {
}
