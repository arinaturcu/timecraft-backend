using Ardalis.Specification;
using TimeCraft.Core.Entities;

namespace TimeCraft.Core.Specifications;

public sealed class SubtaskSpec : BaseSpec<SubtaskSpec, Subtask>
{
    public SubtaskSpec(Guid id) : base(id)
    {
    }

    public SubtaskSpec(string name)
    {
        Query.Where(e => e.Name == name);
    }
}