using System.Net;
using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Entities;
using TimeCraft.Core.Errors;
using TimeCraft.Core.Requests;
using TimeCraft.Core.Responses;
using TimeCraft.Core.Specifications;
using TimeCraft.Infrastructure.Database;
using TimeCraft.Infrastructure.Repositories.Interfaces;
using TimeCraft.Infrastructure.Services.Interfaces;

namespace TimeCraft.Infrastructure.Services.Implementations;

public class TimeEntryService : ITimeEntryService
{ 
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public TimeEntryService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<TimeEntryDTO>> GetTimeEntry(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new TimeEntryProjectionSpec(id), cancellationToken);

        return result != null ? 
            ServiceResponse<TimeEntryDTO>.ForSuccess(result) : 
            ServiceResponse<TimeEntryDTO>.FromError(CommonErrors.TimeEntryNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<TimeEntryDTO>>> GetTimeEntries(PaginationSearchQueryParams pagination,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new TimeEntryProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<TimeEntryDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<TimeEntryDTO>> AddTimeEntry(TimeEntryAddDTO timeEntry, UserDTO? requestingUser,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse<TimeEntryDTO>.FromError(new(HttpStatusCode.Forbidden, "Not authorized.", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new TimeEntrySpec(timeEntry.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse<TimeEntryDTO>.FromError(new(HttpStatusCode.BadRequest, "Time Entry already exists.", ErrorCodes.CannotAdd));
        }

        var newTimeEntry = new TimeEntry
        {
            Name = timeEntry.Name,
            Description = timeEntry.Description,
            StartTime = timeEntry.StartTime,
            EndTime = timeEntry.EndTime,
            SubtaskId = timeEntry.SubtaskId,
            UserId = requestingUser.Id
        };

        await _repository.AddAsync(newTimeEntry, cancellationToken);

        return ServiceResponse<TimeEntryDTO>.ForSuccess(new ()
        {
            Id = newTimeEntry.Id,
            Name = newTimeEntry.Name,
            Description = newTimeEntry.Description,
            StartTime = newTimeEntry.StartTime,
            EndTime = newTimeEntry.EndTime,
            SubtaskId = newTimeEntry.SubtaskId
        });
    }

    public async Task<ServiceResponse> UpdateTimeEntry(TimeEntryDTO timeEntry, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Not authorized.", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new TimeEntrySpec(timeEntry.Id), cancellationToken);

        if (entity == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Time entry does not exist.", ErrorCodes.CannotUpdate));
        }

        entity.Name = timeEntry.Name;
        entity.Description = timeEntry.Description;

        await _repository.UpdateAsync(entity, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteTimeEntry(Guid id, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Not authorized.", ErrorCodes.CannotDelete));
        }

        var entity = await _repository.GetAsync(new TimeEntrySpec(id), cancellationToken);

        if (entity == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Time Entry does not exist.", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync(new TimeEntrySpec(id), cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}