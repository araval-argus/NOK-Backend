import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNuggetComponent } from './add-nugget.component';

describe('AddNuggetComponent', () => {
  let component: AddNuggetComponent;
  let fixture: ComponentFixture<AddNuggetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddNuggetComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddNuggetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
