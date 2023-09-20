using Domain.Models;
using Domain.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using tasks_api.Controllers;
using TasksApi.Tests.Overwrites;
using TestUtilities;

namespace Tasks.Tests;

public class WorkItemControllerTests
{
    [Theory, AutoMoqData]

    public async Task WorkItemCreate_ShouldReturnId(Mock<IWorkItemService> workItemService, int expectedResult)
    {
        workItemService.Setup(s => s.CreateAsync(It.IsAny<WorkItemModel>(), It.IsAny<string>())).ReturnsAsync(expectedResult);
        var subject = new TestWorkItemController(It.IsAny<ILogger<WorkItemController>>(), workItemService.Object);

        var result = await subject.Create(It.IsAny<WorkItemModel>()) as OkObjectResult;

        workItemService.Verify(c => c.CreateAsync(It.IsAny<WorkItemModel>(), It.IsAny<string>()), Times.Once);

        result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
        result!.Value.Should().BeEquivalentTo(expectedResult);
    }


    [Theory, AutoMoqData]

    public void WorkItemGetAll_ShouldReturnArray(Mock<IWorkItemService> workItemService, WorkItemModel[] expectedResult)
    {
        workItemService.Setup(s => s.GetAll(It.IsAny<string>())).Returns(expectedResult);
        var subject = new TestWorkItemController(It.IsAny<ILogger<WorkItemController>>(), workItemService.Object);

        var result = subject.GetAll() as OkObjectResult;

        workItemService.Verify(c => c.GetAll(It.IsAny<string>()), Times.Once);

        result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
        result!.Value.Should().BeEquivalentTo(expectedResult);
    }


    [Theory, AutoMoqData]

    public void WorkItemGet_ShouldReturnItem(Mock<IWorkItemService> workItemService, WorkItemModel expectedResult)
    {
        workItemService.Setup(s => s.Get(It.IsAny<int>(), It.IsAny<string>())).Returns(expectedResult);
        var subject = new TestWorkItemController(It.IsAny<ILogger<WorkItemController>>(), workItemService.Object);

        var result = subject.Get(It.IsAny<int>()) as OkObjectResult;

        workItemService.Verify(c => c.Get(It.IsAny<int>(), It.IsAny<string>()), Times.Once);

        result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
        result!.Value.Should().BeEquivalentTo(expectedResult);
    }

    [Theory, AutoMoqData]

    internal void WorkItemGet_ShouldReturnNotFound(Mock<IWorkItemService> workItemService)
    {
        workItemService.Setup(s => s.Get(It.IsAny<int>(), It.IsAny<string>())).Returns<WorkItemModel>(null);

        var subject = new TestWorkItemController(It.IsAny<ILogger<WorkItemController>>(), workItemService.Object);

        var result = subject.Get(It.IsAny<int>());

        workItemService.Verify(c => c.Get(It.IsAny<int>(), It.IsAny<string>()), Times.Once);

        result.Should().NotBeNull().And.BeOfType<NotFoundResult>();
    }
}