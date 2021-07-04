export default class TaskModel {
    id: number;
    title: string;
    description: string;
    status: TaskStatus;
    date: Date;

    public static clone(task: TaskModel): TaskModel {
        let newTask = new TaskModel();
        newTask.id = task ? task.id : 0;
        newTask.title = task ? task.title : "";
        newTask.description = task ? task.description : "";
        newTask.status = task ? task.status : TaskStatus.Pending;
        newTask.date = task ? new Date(Date.parse(task.date.toString())) : new Date();
        newTask.date.setHours(0, 0, 0, 0);
        return newTask;
    }

    public static extend(oldTask: TaskModel, newTask: TaskModel): void {
        oldTask.date = new Date(Date.parse(newTask.date.toString()));
        oldTask.date.setHours(0, 0, 0, 0);
        oldTask.description = newTask.description;
        oldTask.id = newTask.id;
        oldTask.status = newTask.status;
        oldTask.title = newTask.title;
    }
}

export enum TaskStatus {
    Pending = 1,
    Approved = 2,
    Rejected = 3
}