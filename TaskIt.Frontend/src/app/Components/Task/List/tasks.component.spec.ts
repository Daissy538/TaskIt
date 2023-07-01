import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TasksComponent } from './tasks.component';

describe('TaskComponent', () => {
  let component: TasksComponent;
  let fixture: ComponentFixture<TasksComponent>;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [],
    declarations: [TasksComponent]
  }));

  it('should create taskComponent', () => {
    fixture = TestBed.createComponent(TasksComponent);
    component = fixture.componentInstance;
    expect(component).toBeTruthy();
  });
});
