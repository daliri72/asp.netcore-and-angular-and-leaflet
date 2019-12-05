import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {APP_CONFIG, IAppConfig} from '../conf/app.config';
import {catchError, map} from 'rxjs/operators';
import {BehaviorSubject, Observable, throwError as observableThrowError} from 'rxjs';
import {LocationType, LocationViewModel} from '../models/location-type.interface';
import {ResultShow} from "../models/result-show.interface";

@Injectable({
  providedIn: 'root'
})
export class Api2Service {

  mapSource = new BehaviorSubject<boolean>(false);
  mapTelecast$ = this.mapSource.asObservable();


  locationSource = new BehaviorSubject<number>(null);
  locationTelecast$ = this.locationSource.asObservable();


  constructor(
    private http: HttpClient,
    @Inject(APP_CONFIG) private appConfig: IAppConfig,
  ) {
  }

  SelectLocation(id): Observable<ResultShow<LocationViewModel>> {
    return this.http.get<ResultShow<LocationViewModel>>(`${this.appConfig.apiEndpoint}/Location/SelectLocation?id=${id}`)
      .pipe(
        map(response => response || {})
        , catchError(this.handleError)
      );
  }


  GetAllLocationType(): Observable<ResultShow<LocationType[]>> {
    const header = new HttpHeaders({'Content-Type': 'application/json'});
    return this.http.get<ResultShow<LocationType[]>>(`${this.appConfig.apiEndpoint}/Location/GetAllLocationType`, {
      headers: header
    })
      .pipe(
        map(response => response || {})
        , catchError(this.handleError)
      );
  }

  SubmitLocation(ticket: LocationViewModel, filesList: FileList): Observable<ResultShow<LocationViewModel>> {
    if (!filesList || filesList.length === 0) {
      // return observableThrowError("Please select a file.");
    }

    const formData: FormData = new FormData();

    for (const key in ticket) {
      if (ticket.hasOwnProperty(key)) {
        formData.append(key, (<any>ticket)[key]);
      }
    }

    if (filesList && filesList.length && filesList.length > 0) {
      for (let i = 0; i < filesList.length; i++) {
        formData.append(filesList[i].name, filesList[i]);
      }
    }


    const headers = new HttpHeaders().set('Accept', 'application/json');
    return this.http
      .post<ResultShow<LocationViewModel>>(`${this.appConfig.apiEndpoint}/Location/SubmitLocation`, formData,
        {
          headers: headers
        }
      )
      .pipe(
        map(response => response || {})
        , catchError(this.handleError)
      );
  }


  GetAllLocation(): Observable<ResultShow<LocationViewModel[]>> {
    const header = new HttpHeaders({'Content-Type': 'application/json'});
    return this.http.get<LocationViewModel[]>(`${this.appConfig.apiEndpoint}/Location/GetAllLocation`, {
      headers: header
    })
      .pipe(
        map(response => response || {})
        , catchError(this.handleError)
      );
  }


  private handleError(error: HttpErrorResponse): Observable<any> {
    console.error('observable error: ', error);
    return observableThrowError(error);
  }


}
