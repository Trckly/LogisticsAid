import { ComponentFixture, TestBed } from '@angular/core/testing';

import TripConfigPageComponent from './trip-config-page.component';

describe('TripConfigPageComponent', () => {
  let component: TripConfigPageComponent;
  let fixture: ComponentFixture<TripConfigPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TripConfigPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TripConfigPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
