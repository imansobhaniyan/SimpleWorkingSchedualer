using SimpleWorkingSchedualer.StorageModels;

using System.Collections.Generic;

namespace SimpleWorkingSchedualer.Models.Task
{
    public class UserTaskResult
    {
        public UserTaskResult(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            TaskResults = user.UserTasks.ConvertAll(task => new TaskResult(task));
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public List<TaskResult> TaskResults { get; set; }
    }
}
