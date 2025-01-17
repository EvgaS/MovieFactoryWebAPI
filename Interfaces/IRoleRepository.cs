using MovieFactoryWebAPI.Models;
using MovieFactoryWebAPI.DTo;

namespace MovieFactoryWebAPI.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<Role> GetRoles();

        ICollection<RoleCSVDto> GetRolesForFile();

        Role? GetRole(int roleId);

        Role? GetRoleWithMaxBudget();

        ICollection<Role>? GetRoleOfAnActor(int actorId);

        Actor? GetActorByRole(string roleName);

        Actor? GetActorByRoleId(int roleId);

        bool RoleExists(int roleId);

        bool CreateRole(Role role);

        bool UpdateRole(Role role);

        bool UpdateRoleAddActor(int roleId, int actorId, Actor actor, Role role);

        bool DeleteRole(Role role);

        bool Save();
    }
}
