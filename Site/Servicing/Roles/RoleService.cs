using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Servicing.Data;

namespace Servicing.Roles
{
    public class RoleService : IRoleService
    {
        public const string AdminRole = "Admin";
        private string _adminRoleId = null;

        private readonly RoleManager<IdentityRole> _manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

        public async Task Create(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name can't be null, empty or contain only whitespaces");
            if (_manager.RoleExists(name))
                throw new ArgumentException("Role with the same name already exists");

            //var taskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);
            //TaskExtensions.Unwrap(taskFactory.StartNew<Task>(() => _manager.CreateAsync(new IdentityRole(name)))).GetAwaiter().GetResult();
            await _manager.CreateAsync(new IdentityRole(name)).ConfigureAwait(false);
        }

        public async Task Delete(string roleName)
        {
            var roles = _manager.Roles.Where(x => x.Name == roleName).ToArray();
            using (var dc = new AnykeyDbCntext())
            {
                foreach (var identityRole in roles)
                {
                    if (identityRole.Users.Count != 0)
                        throw new InvalidOperationException(string.Format("Role '{0}' is not empty.", identityRole.Name));
                    _manager.Delete(identityRole);
                }
                await dc.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public bool IsAdmin(string[] roleIds)
        {
            if (_adminRoleId == null)
            {
                _adminRoleId = _manager.Roles.Where(x => x.Name == AdminRole).Select(x => x.Id).FirstOrDefault();
            }
            return roleIds.Contains(_adminRoleId);
        }

        public RoleModel[] List()
        {
            return _manager.Roles.Select(x => new RoleModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToArray();
        }

        public RoleModel GetByName(string name)
        {
            return _manager.Roles.Select(x => new RoleModel
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefault(x => x.Name == name);
        }
    }
}
