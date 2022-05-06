using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorChat;
using BlazorStrap;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

if (!builder.Services.Any(x => x.ServiceType == typeof(HttpClient)))
{
// Setup HttpClient for server side in a client side compatible fashion
builder.Services.AddScoped<HttpClient>(s =>
{
    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
    NavigationManager navman = s.GetRequiredService<NavigationManager>();
    return new HttpClient
    {
        BaseAddress = new Uri(navman.BaseUri)
    };
});
}
builder.Services.AddBlazorStrap();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHub<BlazorChatSampleHub>(BlazorChatSampleHub.HubUrl);

app.Run();
