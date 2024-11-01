using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Minimal.Api;
using Minimal.Api.Auth;
using Minimal.Api.Endpoints;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args = args,
    ApplicationName = "Minimal.Api",
    EnvironmentName = Environment.GetEnvironmentVariable("env"),
    WebRootPath = "./wwwroot"
});

// add local json file
builder.Configuration.AddJsonFile("appsettings.Local.json", true, true);

// authentication
builder.Services.AddAuthentication(ApiKeySchemeConstants.SchemeName)
    .AddScheme<ApiKeyAuthSchemeOptions, ApiKeyAuthHandler>(ApiKeySchemeConstants.SchemeName, _ => { });
builder.Services.AddAuthorization();

// add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure json options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

// add endpoints
builder.Services.AddMinimalEndpoints<Program>(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapGet("/", () => "Hello World!");
app.MapGet("hello", () => "Hello from /hello endpoint!"); // / is added implicitly
app.MapMethods("world", new[] { "PATCH", "HEAD", "OPTIONS" }, () => "Hello from /world endpoint!");

// automatically injected
app.MapGet("claims", [Authorize(AuthenticationSchemes = ApiKeySchemeConstants.SchemeName)]
    (ClaimsPrincipal user) => Results.Ok(user.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.Name)?.Value));
app.MapGet("cancel", (CancellationToken ct) => Results.Ok());

app.MapGet("point", (MapPoint point) => Results.Ok(point));

app.MapGet("some-prop", (ILogger<Program> logger, LinkGenerator linker, HttpContext httpContext) =>
    {
        var someProp = builder.Configuration["SomeProp"];
        logger.LogInformation(someProp);
        // 1.
        // var path = linker.GetPathByName("Hello", new {name = someProp});
        // 2.
        // var path = linker.GetUriByName(httpContext, "Hello", new { name = someProp });
        // return Results.Created(path, someProp);
        // 3.
        return Results.CreatedAtRoute("hello", new {name = someProp});
        // in location header you'll get something like this
        // location: https://localhost:7282/hello/local
    })
    .RequireAuthorization();

app.MapGet("html", () => Results.Extensions.Html("<h1>Hello from HTML</h1>"));

app.UseMinimalEndpoints<Program>();

// 1.
// var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
// app.Run($"https://localhost:{port}");
// 2.
// app.Urls.Add("https://localhost:7285");
app.Run();