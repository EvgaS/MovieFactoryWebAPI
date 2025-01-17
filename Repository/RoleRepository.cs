using Microsoft.EntityFrameworkCore;
using MovieFactoryWebAPI.Data;
using MovieFactoryWebAPI.DTo;
using MovieFactoryWebAPI.Interfaces;
using MovieFactoryWebAPI.Models;

namespace MovieFactoryWebAPI.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRole(Role role)
        {
            _context.Add(role);
            return Save();
        }

        public bool DeleteRole(Role role)
        {
            _context.Remove(role);
            return Save();
        }

        public Role? GetRole(int roleId)
        {
            return _context.Roles.Where(o => o.RoleId == roleId).FirstOrDefault();
        }

        public Role? GetRoleWithMaxBudget()
        {
            return _context.Roles.FromSqlRaw("SELECT * FROM Roles WHERE Budget = (SELECT MAX(Budget) FROM Roles)").FirstOrDefault();
        }

        public ICollection<Role>? GetRoleOfAnActor(int actorId)
        {

            return _context.Roles.FromSqlRaw("SELECT * FROM Roles").OrderBy(x => x.RoleName).ToList();
        }

        public ICollection<Role> GetRoles()
        {
            return _context.Roles.ToList() ?? new List<Role>();
        }

        public Actor? GetActorByRole(string roleName)
        {
            return _context.Roles.Where(x => x.RoleName.Equals(roleName.UnifyTheString())).Select(c => c.Actor).FirstOrDefault();
        }

        public Actor? GetActorByRoleId(int roleId)
        {
            return _context.Roles.Where(x => x.RoleId == roleId).Select(c => c.Actor).FirstOrDefault();
        }

        public bool RoleExists(int roleId)
        {
            return _context.Roles.Any(o => o.RoleId == roleId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRole(Role role)
        {
            _context.Update(role);
            return Save();
        }

        public bool UpdateRoleAddActor(int roleId, int actorId, Actor actor, Role role)
        {
            role.Actor = actor;
            _context.Update(role);
            return Save();
        }       

        public ICollection<RoleCSVDto> GetRolesForFile()
        {
            var query = from a in _context.Roles
                        join b in _context.Movies on a.MovieInfoKey equals  b.MovieId
                        join c in _context.Actors on a.ActorInfoKey equals c.ActorId
                        select new RoleCSVDto
                        {
                           RoleName = a.RoleName,
                           RoleDescription = a.RoleDescription,
                           Budget = a.Budget,
                           MovieName = b.MovieName,      
                           ActorName = c.ActorName,
                        };

            return query.ToList();
        }
    }
}
