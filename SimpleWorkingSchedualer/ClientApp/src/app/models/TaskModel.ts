export default class TaskModel {
    id: number;
    title: string;
    description: string;
    status: TaskStatus;
    date: Date;
}

export enum TaskStatus {
    Pending = 1,
    Approved = 2,
    Rejected = 3
}