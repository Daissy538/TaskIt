import { Task } from 'src/app/Domain/task';
import { TaskService } from '../task.service';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';

@Component({
  selector: 'tasks',
  templateUrl: './tasks.component.html'
})
export class TasksComponent implements OnInit, OnDestroy{

  @Input()
  public width: number | undefined;

  @Input()
  public heigth: number | undefined;
  private readonly _displayedColumns: string[];
  public get displayedColumns(): string[]{
    return this._displayedColumns;
  }

  private _tasks: Task[];
  public get tasks(): Task[]{
    return this._tasks;
  }

  constructor(private _taskService:TaskService,
              private _router: Router) {
    this._tasks = [];
    this._displayedColumns = ['title', 'description', 'endDate'];

  }

  ngOnInit(): void {
    this.getTasks();
  }

  ngOnDestroy(): void {
  }

  public openTask(task: Task): void {
    this._router.navigate(['/task/', task.id]);
  }

  private getTasks(): void {
    this._taskService.getTasks()
    .subscribe((response) => {
      this._tasks = response;
    });
  }

}
