using Microsoft.AspNetCore.SignalR;

using SimpleWorkingSchedualer.Hubs;
using SimpleWorkingSchedualer.Models.Task;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWorkingSchedualer.Services
{
    public class DefaultHubClient
    {
        private readonly IHubContext<DefaultHub> defaultHub;

        public DefaultHubClient(IHubContext<DefaultHub> defaultHub)
        {
            this.defaultHub = defaultHub;
        }

        public async Task AddOrUpdateTask(TaskResult result, UserTaskResult userTaskResult)
        {
            await defaultHub.Clients.All.SendAsync("addOrUpdateTask", userTaskResult, result);
        }

        public async Task UpdateStatus(TaskResult result)
        {
            await defaultHub.Clients.All.SendAsync("updateStatus", result);
        }
    }
}
