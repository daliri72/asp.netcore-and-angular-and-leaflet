import {Component, Injector, OnInit} from '@angular/core';

import {icon, latLng, marker, Marker, tileLayer} from 'leaflet';
import {AppComponentBase} from "../../core/base/app-base.component";
import {LocationViewModel} from "../../core/models/location-type.interface";

@Component({
  selector: 'app-show-location',
  templateUrl: './show-location.component.html',
  styleUrls: ['./show-location.component.css']
})
export class ShowLocationComponent extends AppComponentBase implements OnInit {

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

  listLocation: LocationViewModel[] = [];

  constructor(
    injector: Injector) {
    super(injector);
  }

  GetAllLocation = () => {
    this.apiService.GetAllLocation()
      .subscribe(data => {
        console.log('GetAllLocation', data);
        if (data.status) {
          this.listLocation = data.dataValueObject;
          if (this.listLocation && this.listLocation.length && this.listLocation.length > 0) {
            this.listLocation.forEach(data => {
              this.createMap(data);
            });
          }

        }
        if (!data.status) {
          this.sweetalertService.serverError();
        }
      });
  }



  createMap(item: LocationViewModel) {


    if (item && item.lat && item.lng) {

      const popupInfo = `
            <div class="card border-success mb-3" style="max-width: 18rem;">
              <div class="card-header bg-success">${item.locationName}</div>
              <div class="card-body">
                <h5 class="card-title"> Location Type : ${item.locationTypeName}</h5>                
              </div>
              <div class="card-footer">
                <button class="btn btn-xs btn-info" id="${item.id}" >Edit</button>
<!--                <a class="btn btn-xs btn-warning" href="#close">Close</a>-->
              </div>             
            </div>`;

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

      this.markers.push(newMarker);
    }
  }

  ngOnInit() {


    this.apiService.mapTelecast$.subscribe(data => {
      if (data) {
        this.GetAllLocation();
      }
    })
  }

}
