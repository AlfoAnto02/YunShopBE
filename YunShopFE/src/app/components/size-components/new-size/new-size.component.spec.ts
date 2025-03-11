import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewSizeComponent } from './new-size.component';

describe('NewSizeComponent', () => {
  let component: NewSizeComponent;
  let fixture: ComponentFixture<NewSizeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewSizeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewSizeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
