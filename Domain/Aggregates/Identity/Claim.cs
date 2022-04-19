
namespace Domain.Aggregates.Identity
{
    public class Claim : EntityBase<int>
    {
        public string Name { get; private set; }

        public Claim(string name)
        {
            Name = name;
        }

        public Claim(int id, string name) : base(id)
        {
            Name = name;
        }
    }
}
