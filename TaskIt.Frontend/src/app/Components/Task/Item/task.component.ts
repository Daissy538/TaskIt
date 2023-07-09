import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { EMPTY, switchMap } from "rxjs";
import { Task } from "src/app/Domain/task";
import { TaskService } from "../task.service";

@Component({
  selector: 'task',
  templateUrl: './task.component.html'
})
export class TaskComponent implements OnInit, OnDestroy{

  @Input()
  public task : Task | undefined;

  constructor() {
  }

  ngOnInit(): void {;
  }

  ngOnDestroy(): void {
  }
}
