using System.Linq.Expressions;
using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Entities;

using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;

namespace TimeCraft.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the user entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class UserSettingsSpec : BaseSpec<UserSettingsSpec, UserSettings>
{
    public UserSettingsSpec()
    {
        Query.Where(e => EF.Functions.ILike(e.UserId.ToString(), "%" )); 
    }
    
    public UserSettingsSpec(Guid userId)
    {
        Query.Where(e => e.UserId == userId);
    }
}