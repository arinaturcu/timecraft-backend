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

public class ClientService : IClientService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    
    public ClientService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    
    public async Task<ServiceResponse<ClientDTO>> GetClient(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ClientProjectionSpec(id), cancellationToken);
        
        return result != null ? 
            ServiceResponse<ClientDTO>.ForSuccess(result) : 
            ServiceResponse<ClientDTO>.FromError(CommonErrors.ClientNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<ClientDTO>>> GetClients(PaginationSearchQueryParams pagination,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ClientProjectionSpec(pagination.Search), cancellationToken);
        
        return ServiceResponse<PagedResponse<ClientDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<ClientDTO>> AddClient(ClientAddDTO client, UserDTO? requestingUser,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse<ClientDTO>.FromError(new(HttpStatusCode.Forbidden, "Not authorized.", ErrorCodes.CannotAdd));
        }
        
        var result = await _repository.GetAsync(new ClientSpec(client.Name), cancellationToken);
        
        if (result != null)
        {
            return ServiceResponse<ClientDTO>.FromError(new(HttpStatusCode.BadRequest, "Client already exists.", ErrorCodes.CannotAdd));
        }

        var newClient = new Client
        {
            Name = client.Name,
            Description = client.Description,
            UserId = requestingUser.Id
        };
        
        await _repository.AddAsync(newClient, cancellationToken);
        
        return ServiceResponse<ClientDTO>.ForSuccess(new ()
        {
            Id = newClient.Id,
            Name = newClient.Name,
            Description = newClient.Description
        });
    }

    public async Task<ServiceResponse> UpdateClient(ClientDTO client, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Not authorized.", ErrorCodes.CannotUpdate));
        }
        
        var entity = await _repository.GetAsync(new ClientSpec(client.Id), cancellationToken);
        
        if (entity == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Client does not exist.", ErrorCodes.CannotUpdate));
        }
        
        entity.Name = client.Name;
        entity.Description = client.Description;
        
        await _repository.UpdateAsync(entity, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteClient(Guid id, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Not authorized.", ErrorCodes.CannotDelete));
        }
        
        var entity = await _repository.GetAsync(new ClientSpec(id), cancellationToken);
        
        if (entity == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Client does not exist.", ErrorCodes.CannotDelete));
        }
        
        await _repository.DeleteAsync(new ClientSpec(id), cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
}