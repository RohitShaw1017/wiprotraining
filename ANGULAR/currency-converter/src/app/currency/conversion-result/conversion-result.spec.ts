import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConversionResult } from './conversion-result';

describe('ConversionResult', () => {
  let component: ConversionResult;
  let fixture: ComponentFixture<ConversionResult>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ConversionResult]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConversionResult);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
