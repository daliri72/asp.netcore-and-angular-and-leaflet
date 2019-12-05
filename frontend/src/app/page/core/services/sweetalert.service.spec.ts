import { TestBed } from '@angular/core/testing';

import { SweetalertService } from './sweetalert.service';

describe('SweetalertService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SweetalertService = TestBed.get(SweetalertService);
    expect(service).toBeTruthy();
  });
});
