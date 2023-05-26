import { Injectable } from '@angular/core';
import { Task } from 'src/app/Domain/task';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TaskAdapterService{
  private readonly URL = "task";

  constructor(private _httpClient: HttpClient) { }

  public getTasks(): Observable<Task[]> {
    let request = this._httpClient.get<Task[]>(this.URL);

    return request;
  }
}
