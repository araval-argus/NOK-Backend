import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NudgetsComponent } from './nudgets.component';

describe('NudgetsComponent', () => {
  let component: NudgetsComponent;
  let fixture: ComponentFixture<NudgetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NudgetsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NudgetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
