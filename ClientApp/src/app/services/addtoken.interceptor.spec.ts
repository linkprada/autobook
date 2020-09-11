import { TestBed } from '@angular/core/testing';

import { AddtokenInterceptor } from './addtoken.interceptor';

describe('AddtokenInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      AddtokenInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: AddtokenInterceptor = TestBed.inject(AddtokenInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
