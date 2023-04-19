using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Entities;

namespace TimeCraft.Core.Specifications;

public sealed class ClientProjectionSpec : BaseSpec<ClientProjectionSpec, Client, ClientDTO>
{
    protected override Expression<Func<Client, ClientDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description ?? string.Empty
    };

    public ClientProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ClientProjectionSpec(Guid id) : base(id)
    {
    }

    public ClientProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr)); // This is an example on who database specific expressions can be used via C# expressions.
        // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}