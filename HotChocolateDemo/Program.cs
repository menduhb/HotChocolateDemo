using HotChocolateDemo.DataLoaders;
using HotChocolateDemo.Mutation;
using HotChocolateDemo.Repository;
using HotChocolateDemo.Schema;
using HotChocolateDemo.Services;
using HotChocolateDemo.Subscription;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    //.AddSubscriptionType<Subscription>()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    ;


builder.Services.AddPooledDbContextFactory<SchoolDbContext>(c =>
c.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<InstructorRepository>();
builder.Services.AddScoped<InstructorDataLoader>();


// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseWebSockets();

app.MapGraphQL();

app.MapControllers();

app.Run();
