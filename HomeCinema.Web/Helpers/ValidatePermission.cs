using HomeCinema.Data.Abstract;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Entities;
using HomeCinema.Services;
using HomeCinema.Web.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Routing;

namespace HomeCinema.Web.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ValidatePermission : AuthorizationFilterAttribute
    {
        private readonly string[] allowedroles;
        private readonly string permissionKey;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IEntityBaseRepository<RolePermission> _rolePermissionRepository;
        //private readonly IEntityBaseRepository<UserPermission> _userPermissionRepository;
        //private readonly IMembershipService _membershipService;

        // Constructeur multiString
        public ValidatePermission(
            //IMembershipService membershipService, IEntityBaseRepository<UserPermission> userPermissionRepository, IEntityBaseRepository<RolePermission> rolePermissionRepository, IUnitOfWork unitOfWork,
            params string[] roles)
        {
            //this._unitOfWork = unitOfWork;
            //this._rolePermissionRepository = rolePermissionRepository;
            //this._userPermissionRepository = userPermissionRepository;
            //this._membershipService = membershipService;

            //Le nom du module / page
            this.permissionKey = roles[0];
            this.allowedroles = roles;
        }
    //    public ValidatePermission(
    //IMembershipService membershipService, IEntityBaseRepository<UserPermission> userPermissionRepository, IEntityBaseRepository<RolePermission> rolePermissionRepository, IUnitOfWork unitOfWork,
    //params string[] roles)
    //    {
    //        this._unitOfWork = unitOfWork;
    //        this._rolePermissionRepository = rolePermissionRepository;
    //        this._userPermissionRepository = userPermissionRepository;
    //        this._membershipService = membershipService;

    //        //Le nom du module / page
    //        this.permissionKey = roles[0];
    //        this.allowedroles = roles;
    //    }
        private const string _authorizedToken = "AuthorizedToken";
        private const string _userAgent = "User-Agent";

        //private UserAuthorizations objAuth = null;

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            string authorizedToken = string.Empty;
            string userAgent = string.Empty;
            IEnumerable<string> authHeaderValues = null;

            try
            {
                //var headerToken = filterContext.Request.Headers.SingleOrDefault(x => x.Key == _authorizedToken);
                //if (headerToken.Key != null)
                filterContext.Request.Headers.TryGetValues("Authorization", out authHeaderValues);
                if (authHeaderValues == null)
                {

                }
                    //return base.SendAsync(request, cancellationToken); // cross fingers

                var tokens = authHeaderValues.FirstOrDefault();
                tokens = tokens.Replace("Basic", "").Trim();
                if (!string.IsNullOrEmpty(tokens))
                {
                    //authorizedToken = Convert.ToString(headerToken.Value.SingleOrDefault());
                    //userAgent = Convert.ToString(filterContext.Request.Headers.UserAgent);
                    if (!IsAuthorize(HttpContext.Current.User.Identity,filterContext, userAgent))
                    {
                        filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        return;
                    }
                }
                else
                {
                    filterContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    return;
                }
            }
            catch (Exception ex)
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                return;
            }

            base.OnAuthorization(filterContext);
        }

        private bool IsAuthorize(IIdentity identity, HttpActionContext acContext, string userAgent)
        {
            var _membershipService = acContext.Request.GetMembershipService();
            var _rolePermissionService = acContext.Request.GetRolePermissionService();

            // Si * alors allowAnonymous
            if (permissionKey == "*")
                return true;

            //IPrincipal user = httpContext.User;
            //IIdentity identity = user.Identity;
            // Si "" ou ? alors necessite une authentification sans filtre par roles ou username
            if (permissionKey == "" || permissionKey == "?")
                if (!identity.IsAuthenticated)
                {
                    return false;
                }
                else
                    return true;
            // if user not logged in redirect to login
            if (!identity.IsAuthenticated)
                return false;
            bool isAuthorized = true;
            //using (FX_Entities context = new FX_Entities())
            //{
            // récupére l'utilisateur actuel from DB
            User CurrentUser = _membershipService.GetUserByName(identity.Name);
            //(from a in context.AspNetUsers
            //                           where a.UserName.ToLower() == user.Identity.Name
            //                           select a).FirstOrDefault();
            //récupére la liste des roles de l'utilisateur
            List<int> UserRoles = _membershipService.GetUserRoles(identity.Name).Select(x => x.ID).ToList();
            //List<string> UserRoles = CurrentUser.AspNetRoles.Select(x => x.Id).ToList();  // Get From DB
            // True si le module est autorisé a un des roles de l'utilisateur
            isAuthorized = _rolePermissionService.ValidateAuth(UserRoles, permissionKey);
            //isAuthorized = _rolePermissionRepository.Any(x => UserRoles.Contains(x.RoleId) && x.PermissionKey == permissionKey);
            // retourne null si l'utilisateur n'a pas d'autorisation specifique a ce module
            UserPermission permission = CurrentUser.UserPermissions.FirstOrDefault(x => x.PermissionKey == permissionKey);
            if (permission != null)
                isAuthorized = permission.Granted;

            return isAuthorized;
        }
    }
}
