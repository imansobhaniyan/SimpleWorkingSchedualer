using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWorkingSchedualer.StorageModels
{
    public class UserTask
    {
        public UserTask()
        {
            UserTaskStatusHistories = new List<UserTaskStatusHistory>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime TaskDate { get; set; }

        public User User { get; set; }

        public List<UserTaskStatusHistory> UserTaskStatusHistories { get; set; }
    }
}
