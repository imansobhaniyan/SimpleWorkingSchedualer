using System;

namespace SimpleWorkingSchedualer.StorageModels
{
    public class UserTaskStatusHistory
    {
        public UserTaskStatusHistory()
        {
            CreateDate = DateTime.Now;
        }

        public int Id { get; set; }

        public int UserTaskId { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public UserTask UserTask { get; set; }

        public enum TaskStatus
        {
            Pending = 1,
            Approved = 2,
            Rejected = 3
        }
    }
}
