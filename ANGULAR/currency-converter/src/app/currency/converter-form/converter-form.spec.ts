import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConverterForm } from './converter-form';

describe('ConverterForm', () => {
  let component: ConverterForm;
  let fixture: ComponentFixture<ConverterForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ConverterForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConverterForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
