using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWorkingSchedualer.DataAccessLayer
{
    public class SimpleWorkingSchedualerDbContext : DbContext
    {
        public SimpleWorkingSchedualerDbContext(DbContextOptions<SimpleWorkingSchedualerDbContext> options) : base(options)
        {
        }

        public DbSet<StorageModels.User> Users { get; set; }

        public DbSet<StorageModels.UserTask> UserTasks { get; set; }
    }
}
