/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UserKycComponent } from './user-Kyc.component';

describe('UserKycComponent', () => {
  let component: UserKycComponent;
  let fixture: ComponentFixture<UserKycComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserKycComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserKycComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
