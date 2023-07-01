import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { EMPTY, switchMap } from "rxjs";
import { Task } from "src/app/Domain/task";
import { TaskService } from "../task.service";

@Component({
  templateUrl: './task.component.html'
})
export class TaskComponent implements OnInit, OnDestroy{

  private _task : Task | undefined;
  public get task(): Task | undefined{
    if(!this._task){
      return undefined;
    }

    return {...this._task};
  }

  constructor(private _route: ActivatedRoute,
              private _taskService: TaskService) {
  }

  ngOnInit(): void {;
    let newSub = this._route.paramMap.pipe(
      switchMap(params => {
      let id = params.get('id');
        if(id){
          console.log('id', id);
          return this._taskService.getTask(id);
        }else{
          return EMPTY;
        }
    }));

    newSub.subscribe(response => {
      this._task = response;
    });
  }

  ngOnDestroy(): void {
  }
}
