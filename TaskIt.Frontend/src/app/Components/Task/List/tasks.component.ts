import { Task } from 'src/app/Domain/task';
import { TaskService } from '../task.service';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';

@Component({
  selector: 'tasks',
  templateUrl: './tasks.component.html'
})
export class TasksComponent implements OnInit, OnDestroy{

  @Input()
  public width: number | undefined;

  @Input()
  public heigth: number | undefined;

  @Output()
  public taskSelected: EventEmitter<Task>;

  private readonly _displayedColumns: string[];
  public get displayedColumns(): string[]{
    return structuredClone(this._displayedColumns);
  }

  private _tasks: Task[];
  public get tasks(): Task[]{
    return structuredClone(this._tasks);
  }

  constructor(private _taskService:TaskService) {
    this._tasks = [];
    this.taskSelected = new EventEmitter<Task>();
    this._displayedColumns = ['title', 'description', 'endDate'];

  }

  ngOnInit(): void {
    this.getTasks();
  }

  ngOnDestroy(): void {
  }

  public onTaskSelected(task: Task): void {
    this.taskSelected.emit(structuredClone(task));
  }

  private getTasks(): void {
    this._taskService.getTasks()
    .subscribe((response) => {
      this._tasks = response;
    });
  }

}
