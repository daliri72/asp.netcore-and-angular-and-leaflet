import {InjectionToken} from '@angular/core';

export let APP_CONFIG = new InjectionToken<string>('app.config');


export interface IAppConfig {
  apiEndpoint: string;
  endpoint: string;
}

export const AppConfig: IAppConfig = {

  apiEndpoint: 'http://localhost:5000/api',
  endpoint: 'http://localhost:5000/',
};
