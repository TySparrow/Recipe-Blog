using Microsoft.EntityFrameworkCore;
using Recipe_Blog.Web.Data;
using Recipe_Blog.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Inject DbContext into the services of the application
builder.Services.AddDbContext<Recipe_BlogDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Recipe-BlogDbConnectionString")));

//Inject into services when someone calls ITagRepository, give them the implementation
builder.Services.AddScoped<ITagRepository, TagRepository>();

//Inject into services when someone calls IBlogPostRepository, give them the implementation
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
