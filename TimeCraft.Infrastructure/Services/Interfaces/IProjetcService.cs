using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Requests;
using TimeCraft.Core.Responses;

namespace TimeCraft.Infrastructure.Services.Interfaces;

public interface IProjetcService
{
    public Task<ServiceResponse<ProjectDTO>> GetProject(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<ProjectDTO>>> GetProjects(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<ProjectDTO>> AddProject(ProjectAddDTO project, UserDTO? requestingUser, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateProject(ProjectDTO project, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteProject(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}