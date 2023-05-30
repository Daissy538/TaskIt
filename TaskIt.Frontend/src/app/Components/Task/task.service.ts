import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Task } from 'src/app/Domain/task';
import { TaskAdapterService } from 'src/app/Infrastructure/task-adapter.service';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  constructor(private taskAdapterService:  TaskAdapterService) { }

  public getTasks(): Observable<Task[]>{

    return this.taskAdapterService.getTasks();
  }
}
