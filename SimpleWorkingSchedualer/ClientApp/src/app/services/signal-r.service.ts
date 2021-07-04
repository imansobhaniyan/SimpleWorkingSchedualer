import { Inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr'
import TaskModel from '../models/TaskModel';


@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: signalR.HubConnection;

  private baseUrl: string;

  constructor(@Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.baseUrl + 'hub')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public onUpdateStatus(callback: (taskModel: TaskModel) => void): void {
    this.hubConnection.on('updateStatus', param => {
      callback(param);
    });
  }
}
