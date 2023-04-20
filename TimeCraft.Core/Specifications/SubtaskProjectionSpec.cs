using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Entities;

namespace TimeCraft.Core.Specifications;

public sealed class SubtaskProjectionSpec : BaseSpec<SubtaskProjectionSpec, Subtask, SubtaskDTO>
{
    protected override Expression<Func<Subtask, SubtaskDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description ?? string.Empty,
        ProjectId = e.ProjectId
    };

    public SubtaskProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public SubtaskProjectionSpec(Guid id) : base(id)
    {
    }

    public SubtaskProjectionSpec(string? search)
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