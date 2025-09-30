using EmployeeSubgraph.Data;
using EmployeeSubgraph.Services;
using EmployeeSubgraph.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<EmployeeService>();

builder.Services
    .AddGraphQLServer()
    .AddApolloFederation()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<User>();

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
