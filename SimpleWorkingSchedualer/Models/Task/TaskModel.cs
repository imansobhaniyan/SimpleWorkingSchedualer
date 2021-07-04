using System;

namespace SimpleWorkingSchedualer.Models.Task
{
    public class TaskModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public DateTime Date { get; set; }
    }
}
