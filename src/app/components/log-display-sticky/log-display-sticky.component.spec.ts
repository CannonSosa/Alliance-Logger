import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogDisplayStickyComponent } from './log-display-sticky.component';

describe('LogDisplayStickyComponent', () => {
  let component: LogDisplayStickyComponent;
  let fixture: ComponentFixture<LogDisplayStickyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LogDisplayStickyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LogDisplayStickyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
