export interface ResultShow<T> {
  dataValue: string;
  message: string;
  status: boolean;
  dataValueObject: T;
  errors: string[];
}


export enum ErrorHandling {
  userpaswrong,
  add,
  edit,
  server,
  upload,
  validation,
  success,
  duplicate,
  nofile,
  successEdite,
  notfound,
  nullValue,
  duplicateCodeMeli,
  sendMessageError,
  maxRequest,
  notAbleAccess,
  errorCalProjectPrice
}
