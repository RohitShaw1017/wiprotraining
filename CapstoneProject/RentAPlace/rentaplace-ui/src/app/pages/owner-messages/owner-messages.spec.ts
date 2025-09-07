import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerMessages } from './owner-messages';

describe('OwnerMessages', () => {
  let component: OwnerMessages;
  let fixture: ComponentFixture<OwnerMessages>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OwnerMessages]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnerMessages);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
