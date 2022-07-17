using Microsoft.AspNetCore.Components.Authorization;

using MudBlazor.Services;

using ErrorHandleExample.Components.Authentication;

#pragma warning disable CA1812

//--------------------------------------------------------------------------------
// Configure builder
//--------------------------------------------------------------------------------
var builder = WebApplication.CreateBuilder();

// Add framework Services.
builder.Services.AddHttpContextAccessor();
// Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

// Add Authentication component.
builder.Services.Configure<CookieAuthenticationSetting>(builder.Configuration.GetSection("Authentication"));
builder.Services.AddScoped<CookieAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<CookieAuthenticationStateProvider>());

//--------------------------------------------------------------------------------
// Configure the HTTP request pipeline
//--------------------------------------------------------------------------------
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// HTTPS redirection
app.UseHttpsRedirection();

// Static files
app.UseStaticFiles();

// Authentication
app.UseAuthentication();
app.UseAuthorization();

// Routing
app.UseRouting();

// API
app.MapControllers();

// Blazor
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Run
app.Run();
