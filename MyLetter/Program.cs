using Microsoft.AspNetCore.Identity;
using MyLetter.EF;
using MyLetter.EF.Models;
using MyLetter.EF.Seed;
using MyLetter.Extinsions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddApplicationServices(config: builder.Configuration);
builder.Services.AddIdentityServices(config: builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region RunSeed
//if (args.Length == 0 || args[0].ToLower() == "seed") {
    //using (IServiceScope scope = app.Services.CreateScope())
    //{
    //    IServiceProvider services = scope.ServiceProvider;
    //    ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
    //        var manager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
    //var roleManager = scope.ServiceProvider.GetService<RoleManager<AppRole>>();

    //try
    //{
    //    SeedData.Initialize(serviceProvider: services);
    //    SeedData.SeedUsers(services, manager, roleManager).Wait();
    //}
    //    catch (Exception ex)
    //    {
    //        ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
    //        logger.LogError(ex, "An error occurred seeding the DB.");
    //    }
    //};
//}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
