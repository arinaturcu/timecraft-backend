using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Entities;

namespace TimeCraft.Core.Specifications;

public sealed class ProjectProjectionSpec : BaseSpec<ProjectProjectionSpec, Project, ProjectDTO>
{
    protected override Expression<Func<Project, ProjectDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description ?? string.Empty,
        ClientId = e.ClientId
    };

    public ProjectProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ProjectProjectionSpec(Guid id) : base(id)
    {
    }

    public ProjectProjectionSpec(string? search)
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
