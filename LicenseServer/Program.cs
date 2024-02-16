#region

using FastEndpoints;
using FastEndpoints.Swagger;
using LicenseServer.Authentication;
using LicenseServer.Core;
using LicenseServer.Core.Services;
using LicenseServer.Database;
using LicenseServer.Database.Entities;
using LicenseServer.Mappers;
using Microsoft.EntityFrameworkCore;
using NSwag;

#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddNpgsql<XContext>(builder.Configuration.GetConnectionString("Main"),
                                     optionsBuilder =>
                                         optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));

builder.Services.AddScoped<EntityRepository<Event>>();
builder.Services.AddScoped<EntityRepository<Product>>();
builder.Services.AddScoped<EntityRepository<License>>();
builder.Services.AddScoped<EntityRepository<ProductFile>>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<LicenseService>();
builder.Services.AddScoped<FileService>();

builder.Services.AddSingleton<ProductMapper>();
builder.Services.AddSingleton<LicenseMapper>();

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc(settings =>
{
    settings.DocumentName = "LicenseServer";

    settings.Title = "LicenseServer";
    settings.Version = "v1";

    settings.AddAuth("License ID", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Type = OpenApiSecuritySchemeType.ApiKey
    });
}, addJWTBearerAuth: false);

builder.Services.AddAuthentication(options =>
                                       options.DefaultScheme = XAuthSchemeConstants.SchemeName
                                  ).AddScheme<XAuthSchemeOptions, XAuthHandler>(XAuthSchemeConstants.SchemeName,
                                                                                    _ => { });

builder.Services.AddCors();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(settings => settings.ConfigureDefaults());

app.Run();
