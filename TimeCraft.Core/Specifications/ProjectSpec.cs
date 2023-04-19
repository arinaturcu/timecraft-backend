using Ardalis.Specification;
using TimeCraft.Core.Entities;

namespace TimeCraft.Core.Specifications;

public sealed class ProjectSpec : BaseSpec<ProjectSpec, Project>
{
    public ProjectSpec(Guid id) : base(id)
    {
    }

    public ProjectSpec(string name)
    {
        Query.Where(e => e.Name == name);
    }
}