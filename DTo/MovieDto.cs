using System.ComponentModel.DataAnnotations;

namespace MovieFactoryWebAPI.DTo
{
    public class MovieDto
    {
        public int MovieId { get;  private set; }

        [Required]
        public string? MovieName { get; set; }

        public decimal ActorFeesActual { get; private set; }

        public decimal ActorFeesBudget { get; private set; }
    }
}
