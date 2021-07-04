using SimpleWorkingSchedualer.StorageModels;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWorkingSchedualer.Models.Task
{
    public class TaskResult
    {
        public TaskResult(UserTask userTask)
        {
            Id = userTask.Id;
            Title = userTask.Title;
            Description = userTask.Description;
            Status = (int)userTask.UserTaskStatusHistories.OrderByDescending(x => x.CreateDate).Select(x => x.Status).FirstOrDefault();
            Date = userTask.TaskDate;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public DateTime Date { get; set; }
    }
}
