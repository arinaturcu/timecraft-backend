using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Requests;
using TimeCraft.Core.Responses;

namespace TimeCraft.Infrastructure.Services.Interfaces;

public interface ITimeEntryService
{
    public Task<ServiceResponse<TimeEntryDTO>> GetTimeEntry(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<TimeEntryDurationDTO>> GetDurationById(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<TimeEntryDTO>>> GetTimeEntries(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<TimeEntryDTO>> AddTimeEntry(TimeEntryAddDTO timeEntry, UserDTO? requestingUser, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateTimeEntry(TimeEntryDTO timeEntry, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteTimeEntry(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}