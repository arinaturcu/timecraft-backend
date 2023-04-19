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
public sealed class UserSettingsProjectionSpec : BaseSpec<UserSettingsProjectionSpec, UserSettings, UserSettingsDTO>
{
    /// <summary>
    /// This is the projection/mapping expression to be used by the base class to get UserDTO object from the database.
    /// </summary>
    protected override Expression<Func<UserSettings, UserSettingsDTO>> Spec => e => new()
    {
        UserId = e.UserId,
        Theme = e.Theme,
        TimeFormat = e.TimeFormat,
        DateFormat = e.DateFormat,
        TimeZone = e.TimeZone
    };
    
    public UserSettingsProjectionSpec()
    {
        Query.Where(e => EF.Functions.ILike(e.UserId.ToString(), "%" )); 
    }
}