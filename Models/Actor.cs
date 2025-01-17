using System.Data;

namespace MovieFactoryWebAPI.Models
{
    public class Actor
    {
        public int ActorId { get; set; }

        public string ActorName { get; set; }

        public ICollection<Role>? Roles { get; set; }
    }
}
