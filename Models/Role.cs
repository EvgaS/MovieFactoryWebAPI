using System.Reflection;

namespace MovieFactoryWebAPI.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public Gender RoleGender { get; set; }

        public string? RoleDescription { get; set; }

        public decimal Budget { get; set; }

        public int MovieInfoKey { get; set; }
        public Movie Movie { get; set; }

        public int? ActorInfoKey { get; set; }
        public Actor? Actor { get; set; }
    }

    public enum Gender { Male, Female, Uknown }
}
