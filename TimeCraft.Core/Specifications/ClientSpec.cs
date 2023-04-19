using Ardalis.Specification;
using TimeCraft.Core.Entities;

namespace TimeCraft.Core.Specifications;

public sealed class ClientSpec : BaseSpec<ClientSpec, Client>
{
    public ClientSpec(Guid id) : base(id)
    {
    }
    
    public ClientSpec(string name)
    {
        Query.Where(e => e.Name == name);
    }
}