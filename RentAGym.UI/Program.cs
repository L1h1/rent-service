//global using MediatR;
//global using RentAGym.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using RentAGym.Persistence;
//using RentAGym.Application;
//using RentAGym.UI.Components;
//using RentAGym.UI;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNetCore.Components.Authorization;

//var builder = WebApplication.CreateBuilder(args);
//// Add services to the container.
//builder.Services
//    .AddApplication()
//    .AddPersistence(builder.Configuration);

//// Add Identity
//builder.Services.AddCascadingAuthenticationState();
//builder.Services.AddScoped<UserAccessor>();
//builder.Services.AddScoped<IdentityRedirectManager>();
//builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

//builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
//    .AddIdentityCookies();

//builder.Services.AddDefaultIdentity<IdentityUser>(options =>
//    {
//        options.SignIn.RequireConfirmedAccount = false; // !!!
//        options.Password.RequireNonAlphanumeric = false;
//        options.Password.RequireDigit = false;
//        options.Password.RequireLowercase = false;
//        options.Password.RequireUppercase = false;
//        options.Password.RequiredLength = 6;
//    })
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddSignInManager()
//    .AddDefaultTokenProviders();
    
//builder.Services.AddAuthorization(options =>
//    {
//        options.AddPolicy("LandLord", policy => policy.RequireClaim("LandLord","true"));
//        options.AddPolicy("User", policy => policy.RequireClaim("User","true"));
//    });

//builder.Services.AddSingleton<IEmailSender, NoOpEmailSender>();



//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents();

//builder.Services.AddControllers();
////    .AddApplicationPart()

//builder.Services.AddRazorPages();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();

//app.UseStaticFiles();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapRazorComponents<App>()
//    .AddInteractiveServerRenderMode();

//app.MapRazorPages();

////DbInitializer.Initialize(app.Services);

//app.Run();
