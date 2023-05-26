import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/Domain/task';

@Component({
  selector: 'tasks',
  templateUrl: './tasks.component.html'
})
export class TaskComponent implements OnInit{

  private readonly _displayedColumns: string[];
  public get displayedColumns(): string[]{
    return this._displayedColumns;
  }

  private _tasks: Task[];
  public get tasks(): Task[]{
    return this._tasks;
  }

  constructor() {
    this._tasks = [];
    this._displayedColumns = ['title', 'description', 'endDate'];

  }

  ngOnInit(): void {
    this.getTasks();
  }

  private getTasks(): void {
    this._tasks.push(new Task('TestTitle', 'Test fdsfsdf', new Date()));
  }


}
