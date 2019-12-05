import {Component, ElementRef, Injector, OnInit, ViewChild} from '@angular/core';
import {icon, latLng, marker, Marker, tileLayer} from 'leaflet';
import {BsModalRef, BsModalService} from 'ngx-bootstrap';
import {AppComponentBase} from '../core/base/app-base.component';
import {LocationType, LocationViewModel} from '../core/models/location-type.interface';


@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent extends AppComponentBase implements OnInit {

  // Open Street Map definitions
  LAYER_OSM = tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 18,
    attribution: 'Open Street Map'
  });

  // Values to bind to Leaflet Directive
  options = {
    layers: [this.LAYER_OSM],
    zoom: 10,
    center: latLng(32.661343, 51.680374)
  };

  markers: Marker[] = [];


  @ViewChild('itemMapModal', {static: true}) itemMapModal: ElementRef;
  modalRefMapModal: BsModalRef;


  listLocationType: LocationType[] = [];
  model: LocationViewModel = {} as any;
  @ViewChild('screenshotInput', {static: true}) screenshotInput: ElementRef | null = null;


  filesList: FileList;

  constructor(
    injector: Injector) {
    super(injector);
  }

  ngOnInit() {


    /*this.addMarker();*/

    this.apiService.mapSource.next(true);


    this.apiService.locationTelecast$.subscribe(data => {
      if (data != null) {
        // debugger;
        this.editModal(data);
      }

    });

  }

  GetAllLocationType = () => {
    console.log('cal GetAllLocationType');
    this.apiService.GetAllLocationType()
      .subscribe(data => {
        console.log('GetAllLocationType', data);
        if (data.status) {
          if (data.dataValueObject && data.dataValueObject.length) {
            this.listLocationType = data.dataValueObject;
          }
        }
        if (!data.status) {
          this.sweetalertService.serverError();
        }

      });
  };

  editModal = (id) => {
    if (id && id != null) {
      this.apiService.SelectLocation(id)
        .subscribe(data => {
          if (data.status) {
            this.model = data.dataValueObject;
            this.showLocationOnMap(this.model);
            this.openModal();
          }
          if (!data.status) {
            this.sweetalertService.error('null value');
            ;
          }
        })
    }
  }

  showLocationOnMap(item: LocationViewModel) {


    if (item && item.lat && item.lng) {

      const popupInfo = `<b   style="color: red; background-color: white">${
        `${item.locationName} `
      }</b>`;

      const newMarker = marker(
        [+item.lat, +item.lng],
        {
          icon: icon({
            iconSize: [25, 41],
            iconAnchor: [13, 41],
            //  iconUrl: '../../../assets/a.png',
            // iconUrl: '2273e3d8ad9264b7daa5bdbf8e6b47f8.png',
            // shadowUrl: '44a526eed258222515aa21eaffd14a96.png'
            iconUrl: '../../../assets/2273e3d8ad9264b7daa5bdbf8e6b47f8.png',

          })
        }
      ).bindPopup(popupInfo);

      this.markers.pop();
      this.markers.push(newMarker);
    }
  }


  openModal = () => {
    this.GetAllLocationType();

    this.modalRefMapModal = this.bsModalService.show(
      this.itemMapModal,
      {
        backdrop: 'static',
        class: 'modal-lg',
        animated: true,
        keyboard: true,
        ignoreBackdropClick: false
      });
  };


  removeMarker() {
    this.markers.pop();
  }

  handleEvent(eventType: string, e: any) {
    console.log(eventType, e);


    if (e.latlng && e.latlng.lat && e.latlng.lng) {
      const newMarker = marker(
        [e.latlng.lat, e.latlng.lng],
        {
          icon: icon({
            iconSize: [25, 41],
            iconAnchor: [13, 41],
            //  iconUrl: '../../../assets/a.png',
            // iconUrl: '2273e3d8ad9264b7daa5bdbf8e6b47f8.png',
            // shadowUrl: '44a526eed258222515aa21eaffd14a96.png'
            iconUrl: '../../../assets/2273e3d8ad9264b7daa5bdbf8e6b47f8.png',

          })
        }
      );

      this.markers.pop();
      this.markers.push(newMarker);

      this.model.lat = e.latlng.lat;
      this.model.lng = e.latlng.lng;

    }
  }


  fileChange(event: any) {
    this.filesList = event.target.files;
    console.log("fileChange() -> filesList", this.filesList);
  }

  save() {
    this.apiService.SubmitLocation(this.model, this.filesList)
      .subscribe(data => {
        if (data.status) {
          this.sweetalertService.successfullSubmitted();
          this.model = data.dataValueObject;
          this.modalRefMapModal.hide();
          // refresh map

          this.apiService.mapSource.next(true);

        }
        if (!data.status) {
          this.sweetalertService.serverError();
        }
      });


  }


}
