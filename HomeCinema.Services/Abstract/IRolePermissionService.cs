using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Services.Abstract
{
    public interface IRolePermissionService
    {
        bool ValidateAuth(List<int> UserRoles, string permissionKey);
    }
}
