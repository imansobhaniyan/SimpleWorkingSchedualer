import { Injectable } from '@angular/core';
import TaskModel from '../models/TaskModel';
import { HttpClientHelper } from '../utilities/HttpClientHelper';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private httpClient: HttpClientHelper;

  constructor(httpClient: HttpClientHelper) {
    this.httpClient = httpClient;
  }

  public getAllTasks(callback: (tasks: TaskModel[]) => void) {
    this.httpClient.get<TaskModel[]>('api/task', callback);
  }
}
