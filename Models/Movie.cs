namespace MovieFactoryWebAPI.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string MovieName { get; set; }

        public enum Status
        {
            concept, scripted, cast, inProduction, inTheRun, Realized
        }

        public Status MovieStatus { get; private set; } = Status.concept;

        public decimal ActorFeesActual { get; private set; }

        public decimal ActorFeesBudget { get; private set; }

        public string? scriptUrl { get; private set; }

        public void AddScriptUrl(string _scriptUrl)
        {
            scriptUrl = _scriptUrl;
            MovieStatus = Status.scripted;//Is this approach acceptable or should this change occur only through the use of events
        }

        public List<Role> Roles { get; set; } = new();//
    }
}
