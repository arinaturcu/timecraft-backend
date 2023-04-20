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

public class SubtaskService : ISubtaskService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public SubtaskService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<SubtaskDTO>> GetSubtask(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new SubtaskProjectionSpec(id), cancellationToken);

        return result != null ? 
            ServiceResponse<SubtaskDTO>.ForSuccess(result) : 
            ServiceResponse<SubtaskDTO>.FromError(CommonErrors.SubtaskNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<SubtaskDTO>>> GetSubtasks(PaginationSearchQueryParams pagination,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new SubtaskProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<SubtaskDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<SubtaskDTO>> AddSubtask(SubtaskAddDTO subtask, UserDTO? requestingUser,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse<SubtaskDTO>.FromError(new(HttpStatusCode.Forbidden, "Not authorized.", ErrorCodes.CannotAdd));
        }

        var project = await _repository.GetAsync(new ProjectSpec(subtask.ProjectId), cancellationToken);
        if (project == null)
        {
            return ServiceResponse<SubtaskDTO>.FromError(new(HttpStatusCode.BadRequest, "Project does not exist.", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new SubtaskSpec(subtask.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse<SubtaskDTO>.FromError(new(HttpStatusCode.BadRequest, "Subtask already exists.", ErrorCodes.CannotAdd));
        }

        var newSubtask = new Subtask
        {
            Name = subtask.Name,
            Description = subtask.Description,
            ProjectId = subtask.ProjectId,
            UserId = requestingUser.Id
        };

        await _repository.AddAsync(newSubtask, cancellationToken);

        return ServiceResponse<SubtaskDTO>.ForSuccess(new ()
        {
            Id = newSubtask.Id,
            Name = newSubtask.Name,
            Description = newSubtask.Description,
            ProjectId = newSubtask.ProjectId
        });
    }

    public async Task<ServiceResponse> UpdateSubtask(SubtaskDTO subtask, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Not authorized.", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new SubtaskSpec(subtask.Id), cancellationToken);

        if (entity == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Subtask does not exist.", ErrorCodes.CannotUpdate));
        }

        entity.Name = subtask.Name;
        entity.Description = subtask.Description;
        entity.ProjectId = subtask.ProjectId;

        await _repository.UpdateAsync(entity, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteSubtask(Guid id, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Not authorized.", ErrorCodes.CannotDelete));
        }

        var entity = await _repository.GetAsync(new SubtaskSpec(id), cancellationToken);

        if (entity == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Subtask does not exist.", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync(new SubtaskSpec(id), cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
