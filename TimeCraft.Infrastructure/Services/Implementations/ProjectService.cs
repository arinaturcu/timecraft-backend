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

public class ProjectService : IProjetcService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public ProjectService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<ProjectDTO>> GetProject(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ProjectProjectionSpec(id), cancellationToken);

        return result != null ? 
            ServiceResponse<ProjectDTO>.ForSuccess(result) : 
            ServiceResponse<ProjectDTO>.FromError(CommonErrors.ProjectNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<ProjectDTO>>> GetProjects(PaginationSearchQueryParams pagination,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ProjectProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<ProjectDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<ProjectDTO>> AddProject(ProjectAddDTO project, UserDTO? requestingUser,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse<ProjectDTO>.FromError(new(HttpStatusCode.Forbidden, "Not authorized.", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new ProjectSpec(project.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse<ProjectDTO>.FromError(new(HttpStatusCode.BadRequest, "Project already exists.", ErrorCodes.CannotAdd));
        }

        var newProject = new Project
        {
            Name = project.Name,
            Description = project.Description,
            ClientId = project.ClientId,
            UserId = requestingUser.Id
        };

        await _repository.AddAsync(newProject, cancellationToken);

        return ServiceResponse<ProjectDTO>.ForSuccess(new ()
        {
            Id = newProject.Id,
            Name = newProject.Name,
            Description = newProject.Description,
            ClientId = newProject.ClientId
        });
    }

    public async Task<ServiceResponse> UpdateProject(ProjectDTO project, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Not authorized.", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new ProjectSpec(project.Id), cancellationToken);

        if (entity == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Project does not exist.", ErrorCodes.CannotUpdate));
        }

        entity.Name = project.Name;
        entity.Description = project.Description;

        await _repository.UpdateAsync(entity, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteProject(Guid id, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Not authorized.", ErrorCodes.CannotDelete));
        }

        var entity = await _repository.GetAsync(new ProjectSpec(id), cancellationToken);

        if (entity == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Project does not exist.", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync(new ProjectSpec(id), cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
