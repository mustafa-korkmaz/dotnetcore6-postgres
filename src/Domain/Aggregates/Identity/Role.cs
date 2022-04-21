
namespace Domain.Aggregates.Identity
{
    public class Role : EntityBase<int>
    {
        public string Name { get; private set; }

        private ICollection<RoleClaim> _claims;
        public IReadOnlyCollection<RoleClaim> Claims
        {
            get => _claims.ToList();
            private set => _claims = value.ToList();
        }

        public Role(string name)
        {
            _claims = new List<RoleClaim>();
            Name = name;
        }

        public Role(int id, string name) : base(id)
        {
            _claims = new List<RoleClaim>();
            Name = name;
        }
    }
}
