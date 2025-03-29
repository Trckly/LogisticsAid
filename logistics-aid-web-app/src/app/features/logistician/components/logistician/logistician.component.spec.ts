import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogisticianComponent } from './logistician.component';

describe('LogisticianComponent', () => {
  let component: LogisticianComponent;
  let fixture: ComponentFixture<LogisticianComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogisticianComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LogisticianComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
