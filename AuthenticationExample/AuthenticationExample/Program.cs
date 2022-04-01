using AuthenticationExample.Components.Authentication;

using Microsoft.AspNetCore.Components.Authorization;

#pragma warning disable CA1812

var builder = WebApplication.CreateBuilder(args);

// Add framework Services.
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add Authentication component.
builder.Services.Configure<CustomAuthenticationSetting>(builder.Configuration.GetSection("Authentication"));
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<CustomAuthenticationStateProvider>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

// Authentication
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
