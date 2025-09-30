using Microsoft.EntityFrameworkCore;
using UserSubgraph.Data;
using UserSubgraph.Services;
using UserSubgraph.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register your UserServiceSource with dependency injection
builder.Services.AddScoped<UserService>();

// Add GraphQL server
builder.Services
    .AddGraphQLServer()
    .AddApolloFederation()
    .AddQueryType<Query>()       // root query
    .AddMutationType<Mutation>(); // root mutation

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("https://studio.apollographql.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
