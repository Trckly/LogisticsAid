import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogisticiansPageComponent } from './logisticians-page.component';

describe('LogisticiansPageComponent', () => {
  let component: LogisticiansPageComponent;
  let fixture: ComponentFixture<LogisticiansPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogisticiansPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LogisticiansPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
