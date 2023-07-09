import { Component, OnInit } from "@angular/core";
import { Task } from "src/app/Domain/task";

@Component({
  templateUrl: './dashboard-page.component.html'
})
export class DashboardComponent {

  private _selectedTask: Task | undefined;
  public get selectedTask(): Task | undefined {
    return structuredClone(this._selectedTask);
  }

  constructor() {}

  onTaskSelected(task: Task){
    this._selectedTask = task;
  }


}
