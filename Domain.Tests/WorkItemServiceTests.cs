using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Domain.Validators;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using TestUtilities;

namespace Domain.Tests;

public class WorkItemServiceTests
{
    [Theory, AutoMoqData]
    public async Task CreateAsync_ReturnsId(
        Mock<IBaseRepository<WorkItem>> repository,
        Mock<IMapper> mapper,
        Mock<IValidator<WorkItem>> validator,
        WorkItem workItem)
    {
        var subject = new WorkItemService(repository.Object, mapper.Object, validator.Object);

        var workItemModel = new WorkItemModel()
        {
            Id = workItem.Id
        };

        validator.Setup(v => v.Validate(It.IsAny<WorkItem>())).Returns(new ValidationResult());
        mapper.Setup(m => m.Map<WorkItem>(It.IsAny<WorkItemModel>())).Returns(workItem);

        var result = await subject.CreateAsync(workItemModel, It.IsAny<string>());

        repository.Verify(c => c.AddOrUpdateAsync(It.IsAny<WorkItem>()), Times.Once);
        validator.Verify(c => c.Validate(It.IsAny<WorkItem>()), Times.Once);

        result.Should().Be(workItem.Id);
    }

    [Theory, AutoMoqData]
    public async Task CreateAsync_ShouldFailValidation(
        Mock<IBaseRepository<WorkItem>> repository,
        Mock<IMapper> mapper,
        WorkItemModel model)
    {
        var subject = new WorkItemService(repository.Object, mapper.Object, new WorkItemValidator());

        var workItem = new WorkItem();

        mapper.Setup(m => m.Map<WorkItem>(It.IsAny<WorkItemModel>())).Returns(workItem);

        Func<Task> act = async () => { await subject.CreateAsync(model, It.IsAny<string>()); };
        await act.Should().ThrowAsync<WorkItemValidationException>();
    }
}