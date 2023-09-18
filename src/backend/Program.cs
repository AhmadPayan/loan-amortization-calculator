using backend.Services;
using backend.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAmortizationCalculator, AmortizationCalculator>();

builder.Services.AddRazorPages();

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

app.MapRazorPages();
app.UseRouting();

app.Run();