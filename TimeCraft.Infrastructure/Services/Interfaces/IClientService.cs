using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Requests;
using TimeCraft.Core.Responses;

namespace TimeCraft.Infrastructure.Services.Interfaces;

public interface IClientService
{
    public Task<ServiceResponse<ClientDTO>> GetClient(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<ClientDTO>>> GetClients(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<ClientDTO>> AddClient(ClientAddDTO client, UserDTO? requestingUser, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateClient(ClientDTO client, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteClient(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
