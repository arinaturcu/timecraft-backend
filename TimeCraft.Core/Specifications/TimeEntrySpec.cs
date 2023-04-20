using Ardalis.Specification;
using TimeCraft.Core.Entities;

namespace TimeCraft.Core.Specifications;

public sealed class TimeEntrySpec : BaseSpec<TimeEntrySpec, TimeEntry>
{
    public TimeEntrySpec(Guid id) : base(id)
    {
    }

    public TimeEntrySpec(string name)
    {
        Query.Where(e => e.Name == name);
    }
}