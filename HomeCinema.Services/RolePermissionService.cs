using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using HomeCinema.Data.Extensions;
using HomeCinema.Data.Abstract;
using HomeCinema.Services.Abstract;

namespace HomeCinema.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        #region Variables
        private readonly IEntityBaseRepository<RolePermission> _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        public RolePermissionService(
         IEntityBaseRepository<RolePermission> rolePermissionRepository, IUnitOfWork unitOfWork)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _unitOfWork = unitOfWork;
        }

        #region IMembershipService Implementation

        public bool ValidateAuth(List<int> UserRoles, string permissionKey)
        {
            return _rolePermissionRepository.Any(x => UserRoles.Contains(x.RoleId) && x.PermissionKey == permissionKey);
        }

        #endregion


    }
}
