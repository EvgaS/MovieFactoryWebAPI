using LINQtoCSV;
using System.ComponentModel.DataAnnotations;

namespace MovieFactoryWebAPI.DTo
{
    public class RoleDto
    {
        public int RoleId { get; private set; }

        [Required]
        [MinLength(3)]
        [MaxLength(12)]
        [NotEqualStringCustom("string", ErrorMessage = "RoleName must not be 'string'.")]
        public string? RoleName { get; set; }

        [Required]
        [MinLength(12)]
        [MaxLength(120)]
        [NotEqualStringCustom("string", ErrorMessage = "RoleDescription must not be 'string'.")]
        public string? RoleDescription { get; set; }

        public decimal Budget { get; set; }
    }

    public class RoleCSVDto
    {
        public int RoleId { get; private set; }


        [CsvColumn(Name = "RoleName", FieldIndex = 1, CharLength = 10, CanBeNull = false)]
        public string? RoleName { get; set; }


        [CsvColumn(Name = "RoleDescription", FieldIndex = 2, CharLength = 10, CanBeNull = false)]
        public string? RoleDescription { get; set; }


        [CsvColumn(Name = "Budget", FieldIndex = 3, CharLength = 10, CanBeNull = false)]
        public decimal Budget { get; set; }


        [CsvColumn(Name = "MovieName", FieldIndex = 4, CharLength = 10, CanBeNull = false)]
        public string? MovieName { get; set; }


        [CsvColumn(Name = "MovieDescription", FieldIndex = 5, CharLength = 10, CanBeNull = false)]
        public string? MovieDescription { get; set; }


        [CsvColumn(Name = "ActorName", FieldIndex = 6, CharLength = 10, CanBeNull = false)]
        public string? ActorName { get; set; }

    }
}
