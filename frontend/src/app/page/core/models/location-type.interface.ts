export interface LocationType {
  id: number;
  name: string;
}


export interface LocationViewModel {
  id: number;
  locationName: string;
  locationTypeId: number | null;
  logo: string;
  lat: string;
  lng: string;
  label: string;
  draggable: boolean | null;
  locationTypeName: string;
  fileName: string;
}
