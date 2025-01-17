using System.ComponentModel.DataAnnotations;

namespace MovieFactoryWebAPI.DTo
{
    public class ActorDto
    {
        public int ActorId { get; private set; }

        [Required]
        public string? ActorName { get; set; }
    }
}
