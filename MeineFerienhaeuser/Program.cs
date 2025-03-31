using MeineFerienhaeuser;
using MeineFerienhaeuser.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Setze die statischen Werte der AppSettings-Klasse

AppSettings settings = new AppSettings();

settings.DefaultImagePath = configuration["AppSettings:DefaultImagePath"];
settings.DefaultHouseLink = configuration["AppSettings:DefaultHouseLink"];
settings.FailImageText = configuration["AppSettings:FailImageText"];
settings.FailHouseUrl = configuration["AppSettings:FailHouseUrl"];
settings.UrlDaten = configuration["AppSettings:UrlDaten"];
settings.BaseUrlHouse = configuration["AppSettings:BaseUrlHouse"];
settings.BaseUrlLink = configuration["AppSettings:BaseUrlLink"];

// Add services to the container.
builder.Services.AddRazorPages();


//Add the Data
builder.Services.AddSingleton(settings);


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
