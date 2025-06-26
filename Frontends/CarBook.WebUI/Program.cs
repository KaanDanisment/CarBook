using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Adapters.Handlers.ResponseHandlers.Concrete;
using CarBook.WebUI.Services.Abstracts;
using CarBook.WebUI.Services.Concrete;
using CarBook.WebUI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication("CarBookScheme")
    .AddCookie("CarBookScheme", options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.Name = "CarBookAuthCookie";
    });

//Add services to the container
builder.Services.AddTransient<IApiResponseHandler, ApiResponseHandler>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<ITestimonialService, TestimonialService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ITagCloudService, TagCloudService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<IFooterAddressService, FooterAddressService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ISocialMediaService, SocialMediaService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IAuthService, AuthService>();



//SeriLog Configuration
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();


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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
