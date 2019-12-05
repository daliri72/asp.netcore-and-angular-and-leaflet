export interface LocationType {
  id: number;
  name: string;
}


export interface LocationViewModel {
  id: number;
  locationName: string;
  locationTypeId: number | null;

  lat: string;
  lng: string;

  locationTypeName: string;
  fileName: string;
}
