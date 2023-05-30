import { Injectable } from '@angular/core';
import { Task } from 'src/app/Domain/task';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { EnvironmentService } from '../shared/environment.service';

@Injectable({
  providedIn: 'root'
})
export class TaskAdapterService{
  private readonly URL_PATH = "task";
  private servcerURL: string;

  constructor(private _httpClient: HttpClient, private _environmentService: EnvironmentService) {
    this.servcerURL = this._environmentService.getServerUrl() + '/' + this.URL_PATH;
  }

  public getTasks(): Observable<Task[]> {
    let request = this._httpClient.get<Task[]>(this.servcerURL + '/all');

    return request;
  }
}
