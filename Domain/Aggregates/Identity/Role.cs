
namespace Domain.Aggregates.Identity
{
    public class Role : EntityBase<int>
    {
        public string Name { get; private set; }

        public Role(string name)
        {
            Name = name;
        }

        public Role(int id, string name) : base(id)
        {
            Name = name;
        }
    }
}
