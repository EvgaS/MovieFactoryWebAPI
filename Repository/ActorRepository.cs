using MovieFactoryWebAPI.Data;
using MovieFactoryWebAPI.DTo;
using MovieFactoryWebAPI.Interfaces;
using MovieFactoryWebAPI.Models;

namespace MovieFactoryWebAPI.Repository
{
    public class ActorRepository : IActorRepository
    {
        private readonly DataContext _context;

        public ActorRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateActor(Actor actor)
        {
            _context.Add(actor);
            return Save();
        }

        public bool DeleteActor(Actor actor)
        {
            _context.Remove(actor);
            return Save();
        }

        public Actor? GetActor(int id)
        {
            return _context.Actors.Where(p => p.ActorId == id).FirstOrDefault();
        }

        public Actor? GetActor(string name)
        {
            return _context.Actors.FirstOrDefault(p => p.ActorName == name);
        }


        public ICollection<Actor> GetActors()
        {
            return _context.Actors.OrderBy(p => p.ActorId).ToList();
        }

        public Actor? GetActorTrimToUpper(ActorDto actorCreate)
        {
            return GetActors()?.FirstOrDefault(c => c.ActorName.UnifyTheString() == actorCreate.ActorName!.TrimEnd().ToUpper());               
        }

        public bool ActorExists(int actorId)
        {
            return _context.Actors.Any(p => p.ActorId == actorId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateActor(Actor actor)
        {
            _context.Update(actor);
            return Save();
        }

    }

}

