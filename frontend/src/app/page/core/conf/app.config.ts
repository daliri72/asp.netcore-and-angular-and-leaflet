import {InjectionToken} from '@angular/core';

export let APP_CONFIG = new InjectionToken<string>('app.config');


export interface IAppConfig {
  apiEndpoint: string;
  endpoint: string;
}

export const AppConfig: IAppConfig = {

  apiEndpoint: 'https://localhost:5001/api',
  endpoint: 'https://localhost:5001/',
};
