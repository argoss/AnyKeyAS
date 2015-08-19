using System.Threading.Tasks;

namespace Servicing.Permissions
{
    public interface IPermissionService
    {
        Task<PermissionModel> GetPermissions();
    }
}
