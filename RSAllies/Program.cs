using Polly;
using Polly.Extensions.Http;
using RSAllies.Analytics;
using RSAllies.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<SessionService>();

//builder.Services.AddHttpClient<ApiClient>(client =>
//{
//    client.BaseAddress = new Uri("https://roadsafety.southafricanorth.cloudapp.azure.com:5032");
//})
//    .AddPolicyHandler((services, request) =>
//    {
//        return HttpPolicyExtensions
//        .HandleTransientHttpError()
//        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
//    });

//builder.Services.AddHttpClient<ApiService>(client =>
//{
//    client.BaseAddress = new Uri("https://roadsafety.southafricanorth.cloudapp.azure.com:5032");
//})
//    .AddPolicyHandler((services, request) =>
//    {
//        return HttpPolicyExtensions
//        .HandleTransientHttpError()
//        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
//    });

builder.Services.AddHttpClient<ApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5031");
})
    .AddPolicyHandler((services, request) =>
    {
        return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    });

builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5031");
})
    .AddPolicyHandler((services, request) =>
    {
        return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseCors();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
