// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using RentAGym.Domain.Entities;// CureMe.Domain.Entities;
//using CureMe.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Runtime.Intrinsics.X86;

namespace RentAGym.UI.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly IUserEmailStore<IdentityUser> _emailStore;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailSender _emailSender;
    //private readonly IFileService _fileService;

    public RegisterModel(
        UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore,
        SignInManager<IdentityUser> signInManager,
        ILogger<RegisterModel> logger)
        //IEmailSender emailSender
        //IFileService fileService)
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        _signInManager = signInManager;
        _logger = logger;
        //_emailSender = emailSender;
        //_fileService = fileService;
        //Enum.GetNames(typeof(Sex));
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }


    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>

        [Required(ErrorMessage = "Это поле обязательно.")]
        [EmailAddress(ErrorMessage = "Неверный E-mail")]
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required(ErrorMessage = "Это поле обязательно.")]
        [StringLength(100, ErrorMessage = "Минимум 6, максимум 100", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required(ErrorMessage = "Это поле обязательно.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }

        //[DataType(DataType.Date)]
        //public DateOnly DateOfBirth { get; set; }
        //[Required]
        //public Sex Sex { get; set; }

        [Display(Name = "Регистрация арендатора")]
        public bool IsLandLord { get; set; }

        //public IFormFile Avatar { get; set; }
    }


    public async Task OnGetAsync(string returnUrl = null)
    {
        ReturnUrl = returnUrl;
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        if (ModelState.IsValid)
        {
            IdentityUser user = CreateUser();
            user.EmailConfirmed = true;
            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

            IdentityResult result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                // _fileService.CreateFolderForNewPerson(await _signInManager.CreateUserPrincipalAsync(user));

                //if (Input.Avatar != null)
                //{
                //    await _fileService.SaveAvatarAsync(await _signInManager.CreateUserPrincipalAsync(user),
                //        Input.Avatar.OpenReadStream());
                //}

                // Назначить роль "доктор" или "пациент"
                if (Input.IsLandLord)
                {
                    await _userManager.AddClaimsAsync(user, new Claim[] {
                                    new Claim(ClaimTypes.Name, user.UserName),
                                    new Claim(ClaimTypes.Email, user.Email),
                                    new Claim("landLord","true", ClaimValueTypes.Boolean),
                                    new Claim("User","true", ClaimValueTypes.Boolean) });
                }
                else { await _userManager.AddClaimsAsync(user, new Claim[] {
                                    new Claim(ClaimTypes.Name, user.UserName),
                                    new Claim(ClaimTypes.Email, user.Email),
                                    //new Claim(ClaimTypes.NameIdentifier, user2.Id),
                                    new Claim("User","true", ClaimValueTypes.Boolean) }); 
                };

                

                // Подтверждение электронного адреса


                //string userId = await _userManager.GetUserIdAsync(user);
                //string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //string callbackUrl = Url.Page(
                //    "/Account/ConfirmEmail",
                //    null,
                //    new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                //    Request.Scheme);

                //await _emailSender.SendEmailAsync(Input.Email, "Подтвердите регистрацию",
                //    $"Пожалуйста, подтвердите регистрацию <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'> нажав на ссылку.</a>.");


                //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                //{
                //    return RedirectToPage("RegisterConfirmation",
                //        new {
                //            email = Input.Email,
                //            returnUrl// = await GetReturnUrl(await _userManager.FindByEmailAsync(Input.Email))
                //        });
                //}
                //else
                {
                    await _signInManager.SignInAsync(user, false);
                    return Redirect(returnUrl);
                }

                ;
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }

    private IdentityUser CreateUser()
    {
        IdentityUser user;
        try
        {
            if (Input.IsLandLord)
            {
                user = Activator.CreateInstance<Landlord>();
            }
            else
            {
                user = Activator.CreateInstance<Tenant>();
            }
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                                                $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                                                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }

        //user.Sex = Input.Sex;
        //user.DateOfBirth = Input.DateOfBirth;
        return user;
    }

    private IUserEmailStore<IdentityUser> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }

        return (IUserEmailStore<IdentityUser>)_userStore;
    }

    //private async Task<string> GetReturnUrl(IdentityUser user)
    //{
    //    IList<string> roles = await _userManager.GetRolesAsync(user);

    //    return roles[0] switch {
    //        "patient" => "/patients/profile",
    //        "doctor" => "/doctor/doctorPage"
    //    };
    //}
}