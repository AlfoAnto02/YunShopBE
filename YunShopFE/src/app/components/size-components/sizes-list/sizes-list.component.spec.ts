import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SizesListComponent } from './sizes-list.component';

describe('SizesListComponent', () => {
  let component: SizesListComponent;
  let fixture: ComponentFixture<SizesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SizesListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SizesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
