using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Domain.Repositories;
using FluentValidation;

namespace Domain.Services;
public class WorkItemService
{
    private readonly IBaseRepository<WorkItem> _repository;
    private readonly IMapper _mapper;
    IValidator<WorkItem> _validator;

    public WorkItemService(
        IBaseRepository<WorkItem> repository,
        IMapper mapper,
        IValidator<WorkItem> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<int> CreateAsync(WorkItemModel model, string userId)
    {
        var entity = _mapper.Map<WorkItem>(model);

        var validationResult = _validator.Validate(entity);
        if (!validationResult.IsValid)
        {
            throw new WorkItemValidationException(validationResult.Errors);
        }

        entity.UserId = userId;
        await _repository.AddOrUpdateAsync(entity);

        _repository.Save();

        return entity.Id;
    }

    public async Task UpdateAsync(WorkItemModel model, string userId)
    {
        var dbEntity = _repository.Queryable().FirstOrDefault(e => e.Id == model.Id && e.UserId == userId);
        if (dbEntity == null)
        {
            throw new KeyNotFoundException();
        }

        var entity = _mapper.Map<WorkItem>(model);
        var validationResult = _validator.Validate(entity);
        if (!validationResult.IsValid)
        {
            throw new WorkItemValidationException(validationResult.Errors);
        }

        dbEntity.Update(entity);
        await _repository.AddOrUpdateAsync(dbEntity);
        _repository.Save();
    }

    public void Remove(int id, string userId)
    {
        var dbEntity = _repository.Queryable().FirstOrDefault(e => e.Id == id && e.UserId == userId);
        if (dbEntity == null)
        {
            throw new KeyNotFoundException();
        }

        _repository.Remove(dbEntity);
        _repository.Save();
    }

    public WorkItemModel Get(int id, string userId)
    {
        return _mapper.Map<WorkItemModel>(_repository
            .Queryable(false)
            .FirstOrDefault(e => e.Id == id && e.UserId == userId));
    }

    public WorkItemModel[] GetAll(string userId)
    {
        return _mapper.Map<WorkItemModel[]>(_repository
            .Queryable(false)
            .Where(e => e.UserId == userId)
            .OrderBy(e => e.Id)
            .ToArray());
    }
}
