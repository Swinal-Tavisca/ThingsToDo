import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RightviewComponent } from './rightview.component';

describe('RightviewComponent', () => {
  let component: RightviewComponent;
  let fixture: ComponentFixture<RightviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RightviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RightviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
