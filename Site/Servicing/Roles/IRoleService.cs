using System;

namespace Servicing.Roles
{
    public interface IRoleService
    {
        void Create(String name);

        void Delete(String roleName);

        bool IsAdmin(string[] roleIds);

        RoleModel[] List();

        RoleModel GetByName(string name);
    }
}
