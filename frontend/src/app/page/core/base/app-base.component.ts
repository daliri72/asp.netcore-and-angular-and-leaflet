import {BsModalRef, BsModalService} from 'ngx-bootstrap';
import {Api2Service} from '../services/api2.service';
import {Injector} from '@angular/core';
import {SweetalertService} from "../services/sweetalert.service";

export abstract class AppComponentBase {
  bsModalService: BsModalService;
  apiService: Api2Service;
  sweetalertService: SweetalertService;

  constructor(
    injector: Injector
  ) {
    this.bsModalService = injector.get(BsModalService);
    this.apiService = injector.get(Api2Service);
    this.sweetalertService = injector.get(SweetalertService);
  }
}
