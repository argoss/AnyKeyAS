using System;
using System.Threading.Tasks;

namespace Servicing.Roles
{
    public interface IRoleService
    {
        Task Create(String name);

        Task Delete(String roleName);

        bool IsAdmin(string[] roleIds);

        RoleModel[] List();

        RoleModel GetByName(string name);
    }
}
