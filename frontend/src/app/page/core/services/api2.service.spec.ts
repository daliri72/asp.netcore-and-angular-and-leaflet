import { TestBed } from '@angular/core/testing';

import { Api2Service } from './api2.service';

describe('Api2Service', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: Api2Service = TestBed.get(Api2Service);
    expect(service).toBeTruthy();
  });
});
