using Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;
using tasks_api.Controllers;

namespace TasksApi.Tests.Overwrites;
public class TestWorkItemController : WorkItemController
{
    public TestWorkItemController(ILogger<WorkItemController> logger, IWorkItemService workItemService) : base(logger, workItemService)
    {
    }

    protected override string GetUserId()
    {
        return string.Empty;
    }
}
