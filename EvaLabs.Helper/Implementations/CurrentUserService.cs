using System.Security.Principal;
using EvaLabs.Common.Constant;
using EvaLabs.Common.ExtensionMethod;
using EvaLabs.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EvaLabs.Helper.Implementations
{
    public class CurrentUserService<TKey> : ICurrentUserService<TKey>
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var claimsPrincipal = httpContextAccessor?.HttpContext?.User;
            if (claimsPrincipal == null) return;

            UserId = claimsPrincipal.GetFromClaims<TKey>(AppValues.AppClaims.Id);
            Principal = claimsPrincipal;
            IsAuthenticated = claimsPrincipal.Identity?.IsAuthenticated ?? false;
        }

        public TKey UserId { get; }
        public IPrincipal Principal { get; set; }
        public bool IsAuthenticated { get; }
    }
}