import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TaskComponent } from './tasks.component';

describe('TaskComponent', () => {
  let component: TaskComponent;
  let fixture: ComponentFixture<TaskComponent>;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [],
    declarations: [TaskComponent]
  }));

  it('should create taskComponent', () => {
    fixture = TestBed.createComponent(TaskComponent);
    component = fixture.componentInstance;
    expect(component).toBeTruthy();
  });
});
