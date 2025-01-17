using MovieFactoryWebAPI.DTo;
using MovieFactoryWebAPI.Models;

namespace MovieFactoryWebAPI.Interfaces
{
    public interface IActorRepository
    {
        ICollection<Actor>? GetActors();

        Actor? GetActor(int id);

        Actor? GetActor(string name);

        Actor? GetActorTrimToUpper(ActorDto actorCreate);

        bool ActorExists(int actorId);

        bool CreateActor(Actor actor);

        bool UpdateActor(Actor actor);

        bool DeleteActor(Actor actor);

        bool Save();
    }
}
