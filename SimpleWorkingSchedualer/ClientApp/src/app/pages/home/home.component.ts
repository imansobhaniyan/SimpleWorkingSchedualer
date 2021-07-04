import { Component, Input } from '@angular/core';
import { faLongArrowAltDown } from '@fortawesome/free-solid-svg-icons';
import TaskModel, { TaskStatus } from 'src/app/models/TaskModel';
import UserTaskModel from 'src/app/models/UserTaskModel';
import { SignalRService } from 'src/app/services/signal-r.service';
import { TaskService } from 'src/app/services/task.service';
import DateHelper from 'src/app/utilities/DateHelper';
import LocalStorageHelper from 'src/app/utilities/LocalStorageHelper';
import ColumnModel from './models/ColumnModel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  faLongArrowDown = faLongArrowAltDown;

  columns: ColumnModel[] = [];

  weekIndex: number;

  taskService: TaskService;

  userTasks: UserTaskModel[];

  isEditing: boolean;

  isSaving: boolean;

  isRejectSaving: boolean;

  isApproveSaving: boolean;

  saved: boolean;

  editingItem: TaskModel;

  editingUserTask: UserTaskModel;

  isAdmin: boolean;

  constructor(taskService: TaskService, signalRService: SignalRService) {
    this.taskService = taskService;
    this.isAdmin = LocalStorageHelper.getRole() == 1;
    signalRService.startConnection();
    signalRService.onUpdateStatus((updated: TaskModel) => {
      this.statusUpdated(updated);
    });
    this.fillColumns();
    this.loadTasks();
  }

  fillColumns(weekIndex: number = 0): void {
    this.weekIndex = weekIndex;
    this.columns = [];
    var date = DateHelper.getFirstDayOfWeek(weekIndex);
    do {
      this.columns.push(new ColumnModel(date));
      date = DateHelper.addDays(date);
    } while (this.columns.length < 7);
  }

  private loadTasks(): void {
    this.taskService.getAllTasks(userTasks => {
      for (let index = 0; index < userTasks.length; index++) {
        const userTask = userTasks[index];
        for (let taskIndex = 0; taskIndex < userTask.taskResults.length; taskIndex++) {
          const task = userTask.taskResults[taskIndex];
          task.date = new Date(Date.parse(task.date.toString()));
        }
      }
      this.userTasks = userTasks;
    });
  }

  columnClicked(userTask: UserTaskModel, column: ColumnModel): void {
    let existingTask = this.getTask(userTask, column.value);
    if (!existingTask && this.isAdmin)
      return;

    this.editingUserTask = userTask;
    this.isEditing = true;
    this.editingItem = TaskModel.clone(existingTask);
  }

  cancelEditing(): void {
    this.isEditing = false;
  }

  save(): void {
    this.isSaving = true;
    this.taskService.save(this.editingItem, editResult => {
      editResult.date = new Date(Date.parse(editResult.date.toString()));
      if (!this.editingItem.id) {
        this.editingUserTask.taskResults.push(editResult);
      }
      var existingTask = this.getTask(this.editingUserTask, editResult.date);
      TaskModel.extend(existingTask, editResult);
      this.isSaving = false;
      this.saved = true;
      setTimeout(() => {
        this.saved = false;
        this.isEditing = false;
        this.fillColumns(this.weekIndex - 1);
        this.fillColumns(this.weekIndex + 1);
      }, 3000);
    });
  }

  getTask(userTask: UserTaskModel, date: Date): TaskModel {
    for (let userTaskIndex = 0; userTaskIndex < this.userTasks.length; userTaskIndex++) {
      const currentUserTask = this.userTasks[userTaskIndex];
      if (currentUserTask.id == userTask.id)
        for (let index = 0; index < currentUserTask.taskResults.length; index++) {
          const task = currentUserTask.taskResults[index];
          if (task.date.getDate() === date.getDate())
            return task;
        }
    }
  }

  reject(): void {
    this.editingItem.status = TaskStatus.Rejected;
    this.isRejectSaving = true;
    this.updateStatus();
  }

  approve(): void {
    this.editingItem.status = TaskStatus.Approved;
    this.isApproveSaving = true;
    this.updateStatus();
  }

  private updateStatus() {
    this.taskService.setStatus(this.editingItem, editResult => {
      var existingTask = this.getTask(this.editingUserTask, this.editingItem.date);
      existingTask.status = this.editingItem.status;
      this.isApproveSaving = false;
      this.isRejectSaving = false;
      this.saved = true;
      setTimeout(() => {
        this.saved = false;
        this.isEditing = false;
        this.fillColumns(this.weekIndex - 1);
        this.fillColumns(this.weekIndex + 1);
      }, 3000);
    });
  }

  statusUpdated(this: HomeComponent, taskModel: TaskModel): void {
    for (let index = 0; index < this.userTasks.length; index++) {
      const userTask = this.userTasks[index];
      for (let taskIndex = 0; taskIndex < userTask.taskResults.length; taskIndex++) {
        const task = userTask.taskResults[taskIndex];
        if (task.id == taskModel.id)
          task.status = taskModel.status;
      }
    }

    this.fillColumns(this.weekIndex - 1);
    this.fillColumns(this.weekIndex + 1);
  }
}