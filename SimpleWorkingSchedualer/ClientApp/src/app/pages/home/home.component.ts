import { Component, Input } from '@angular/core';
import { faLongArrowAltDown } from '@fortawesome/free-solid-svg-icons';
import TaskModel, { TaskStatus } from 'src/app/models/TaskModel';
import { TaskService } from 'src/app/services/task.service';
import DateHelper from 'src/app/utilities/DateHelper';
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

  tasks: TaskModel[];

  isEditing: boolean;

  isSaving: boolean;

  saved: boolean;

  editingItem: TaskModel;

  @Input() pending: TaskStatus.Pending;
  @Input() approved: TaskStatus.Approved;
  @Input() rejected: TaskStatus.Rejected;

  constructor(taskService: TaskService) {
    this.taskService = taskService;
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
    this.taskService.getAllTasks(tasks => {
      for (let index = 0; index < tasks.length; index++) {
        const task = tasks[index];
        task.date = new Date(Date.parse(task.date.toString()));
      }
      this.tasks = tasks;
    });
  }

  columnClicked(column: ColumnModel): void {
    this.isEditing = true;
    let existingTask = this.getTask(column.value);
    this.editingItem = new TaskModel();
    if (!existingTask) {
      this.editingItem.status = TaskStatus.Pending;
      this.editingItem.date = column.value;
    } else {
      this.editingItem.date = existingTask.date;
      this.editingItem.description = existingTask.description;
      this.editingItem.id = existingTask.id;
      this.editingItem.status = existingTask.status;
      this.editingItem.title = existingTask.title;
    }
  }

  cancelEditing(): void {
    this.isEditing = false;
  }

  save(): void {
    this.isSaving = true;
    this.taskService.save(this.editingItem, editResult => {
      editResult.date = new Date(Date.parse(editResult.date.toString()));
      if (!this.editingItem.id) {
        editResult.date = editResult.date;
        this.tasks.push(editResult);
      }
      var existingTask = this.getTask(editResult.date);
      if (existingTask != null) {
        existingTask.description = editResult.description;
        existingTask.title = editResult.title;
        existingTask.id = editResult.id;
      }
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

  getTask(date: Date): TaskModel {
    for (let index = 0; index < this.tasks.length; index++) {
      const task = this.tasks[index];
      if (task.date.getDate() === date.getDate())
        return task;
    }
  }
}