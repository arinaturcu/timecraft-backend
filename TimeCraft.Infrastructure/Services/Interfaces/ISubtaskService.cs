using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Requests;
using TimeCraft.Core.Responses;

namespace TimeCraft.Infrastructure.Services.Interfaces;

public interface ISubtaskService
{
    public Task<ServiceResponse<SubtaskDTO>> GetSubtask(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<SubtaskDTO>>> GetSubtasks(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<SubtaskDTO>> AddSubtask(SubtaskAddDTO subtask, UserDTO? requestingUser, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateSubtask(SubtaskDTO subtask, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteSubtask(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}