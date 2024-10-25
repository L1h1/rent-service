using Microsoft.AspNetCore.Identity;
using RentAGym.UI.rc2.Identity;

namespace RentAGym.UI.rc2.Data
{
    internal sealed class UserAccessor(
        IHttpContextAccessor httpContextAccessor,
        UserManager<IdentityUser> userManager,
        IdentityRedirectManager redirectManager)
    {
        public async Task<IdentityUser> GetRequiredUserAsync()
        {
            var principal = httpContextAccessor.HttpContext?.User ??
                throw new InvalidOperationException($"{nameof(GetRequiredUserAsync)} requires access to an {nameof(HttpContext)}.");

            var user = await userManager.GetUserAsync(principal);

            if (user is null)
            {
                // Throws NavigationException, which is handled by the framework as a redirect.
                redirectManager.RedirectToWithStatus("/Account/InvalidUser", "Error: Unable to load user with ID '{userManager.GetUserId(principal)}'.");
            }

            return user;
        }
    }
}
