import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeftviewComponent } from './leftview.component';

describe('LeftviewComponent', () => {
  let component: LeftviewComponent;
  let fixture: ComponentFixture<LeftviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeftviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeftviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
