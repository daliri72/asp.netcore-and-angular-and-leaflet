import {Injectable} from '@angular/core';
import swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class SweetalertService {

  constructor() {
  }

  error(text: string) {
    swal({
      title: text,
      type: 'error',
      confirmButtonText: '<i class="fa fa-thumbs-up"></i> OK !'
    });
  }

  serverError() {
    swal({
      title: 'Server Error',
      type: 'error',
      confirmButtonText: '<i class="fa fa-thumbs-up"></i> OK !'
    });
  }

  successfullSubmitted() {

    const msg: string = 'Successfully Submitted';
    swal({
      position: 'top-end',
      type: 'success',
      title: msg,
      showConfirmButton: false,
      timer: 2000
    })

  }
}
