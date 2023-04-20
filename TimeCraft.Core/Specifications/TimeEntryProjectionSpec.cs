using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Entities;

namespace TimeCraft.Core.Specifications;

public sealed class TimeEntryProjectionSpec : BaseSpec<TimeEntryProjectionSpec, TimeEntry, TimeEntryDTO>
{
    protected override Expression<Func<TimeEntry, TimeEntryDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description ?? string.Empty,
        StartTime = e.StartTime,
        EndTime = e.EndTime,
        SubtaskId = e.SubtaskId
    };

    public TimeEntryProjectionSpec(Guid id) : base(id)
    {
    }

    public TimeEntryProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr));
    }
}