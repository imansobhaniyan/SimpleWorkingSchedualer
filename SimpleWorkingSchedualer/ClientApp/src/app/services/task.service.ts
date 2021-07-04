import { Injectable } from '@angular/core';
import TaskModel from '../models/TaskModel';
import DateHelper from '../utilities/DateHelper';
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

  public save(task: TaskModel, callback: (task: TaskModel) => void): void {
    task.date = DateHelper.addDays(task.date);
    this.httpClient.post<TaskModel>('api/task', task, callback);
  }
}
