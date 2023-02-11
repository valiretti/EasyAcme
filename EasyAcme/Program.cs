using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.FluentValidation;
using Blazorise.Icons.FontAwesome;
using EasyAcme.Areas.Identity;
using EasyAcme.DataAccess;
using EasyAcme.HostedServices;
using EasyAcme.Logic;
using EasyAcme.Model.ViewModels;
using EasyAcme.Validators;
using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.Sqlite;
using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorise()
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons()
    .AddBlazoriseFluentValidation();

// Elsa services.
var elsaSection = builder.Configuration.GetSection("Elsa");
builder.Services
    .AddElsa(elsa => elsa
        .UseEntityFrameworkPersistence(ef => ef.UseSqlite(DataAccessExtensions.GetElsaSqLiteConnectionString()), true)
        .AddHttpActivities(elsaSection.GetSection("Server").Bind)
        .AddQuartzTemporalActivities());
builder.Services.AddElsaApiEndpoints();

builder.Services.AddHostedService<UserInitializationHostedService>();
builder.Services.AddScoped<IValidator<CreateAcmeOrderModel>, CreateAcmeOrderValidator>();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddLogicServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();