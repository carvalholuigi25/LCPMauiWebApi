
using Blazored.LocalStorage;
using LCPMauiWebApi.Server.Classes;
using LCPMauiWebApi.Server.Context;
using LCPMauiWebApi.Server.Interfaces;
using LCPMauiWebApi.Server.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MEL = Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using LCPMauiWebApi.Server.Models;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var ismodedb = 0;

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .WriteTo.Async(wt => wt.Console())
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddSingleton<MEL.ILogger>(provider => provider.GetRequiredService<MEL.ILogger<MyUsers>>());
builder.Services.AddSingleton<MEL.ILogger>(provider => provider.GetRequiredService<MEL.ILogger<MyUsersAuth>>());
builder.Services.AddSingleton<MEL.ILogger>(provider => provider.GetRequiredService<MEL.ILogger<Posts>>());
builder.Services.AddSingleton<MEL.ILogger>(provider => provider.GetRequiredService<MEL.ILogger<Comments>>());
builder.Services.AddSingleton<MEL.ILogger>(provider => provider.GetRequiredService<MEL.ILogger<Replies>>());
builder.Services.AddSingleton<MEL.ILogger>(provider => provider.GetRequiredService<MEL.ILogger<Reactions>>());
builder.Services.AddSingleton<MEL.ILogger>(provider => provider.GetRequiredService<MEL.ILogger<Attachments>>());
builder.Services.AddSingleton<MEL.ILogger>(provider => provider.GetRequiredService<MEL.ILogger<Feedback>>());
builder.Services.AddSingleton<MEL.ILogger>(provider => provider.GetRequiredService<MEL.ILogger<Todo>>());

if (ismodedb == 0)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("lcpdb_sqlserver")));
    builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("lcpdb_sqlserver")));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("lcpdb_sqllite")));
    builder.Services.AddDbContext<DBContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("lcpdb_sqllite")));
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddScoped<DbContext, DBContext>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddScoped<IRepliesService, RepliesService>();
builder.Services.AddScoped<IReactionsService, ReactionsService>();
builder.Services.AddScoped<IAttachmentsService, AttachmentsService>();
builder.Services.AddScoped<IFeedbacksService, FeedbacksService>();

builder.Services.AddCors();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    })
    .AddFacebook(fbOptions =>
    {
        fbOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"]!;
        fbOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"]!;
    })
    .AddTwitter(twOptions =>
    {
        twOptions.ConsumerKey = builder.Configuration["Authentication:Twitter:ConsumerKey"]!;
        twOptions.ConsumerSecret = builder.Configuration["Authentication:Twitter:ConsumerSecret"]!;
    })
    .AddGitHub(ghOptions =>
    {
        ghOptions.ClientId = builder.Configuration["Authentication:Github:ClientId"]!;
        ghOptions.ClientSecret = builder.Configuration["Authentication:Github:ClientSecret"]!;
    });
//.AddMicrosoftAccount(micOptions =>
//{
//    micOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"]!;
//    micOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"]!;
//});

//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<HttpContextAccessor>();
//builder.Services.AddHttpClient();
//builder.Services.AddScoped<HttpClient>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LCP Maui Web Api",
        Version = "v1",
        Description = "LCP Maui Web Api is api to produce and serve data for the all kinds of LCP Maui projects.",
        TermsOfService = new Uri("https://localhost:7285/terms"),
        Contact = new OpenApiContact
        {
            Name = "Luis Carvalho",
            Email = "luiscarvalho239@gmail.com",
            Url = new Uri("https://localhost:7285/contacts"),
        },
        License = new OpenApiLicense
        {
            Name = "LCP Maui Web Api",
            Url = new Uri("https://localhost:7285/license"),
        }
    });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

//var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("token").Value!);
//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(x =>
//{
//    x.RequireHttpsMetadata = false;
//    x.SaveToken = true;
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ClockSkew = TimeSpan.Zero
//    };
//});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
}).AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
);
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.UseBlazorFrameworkFiles();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "../Shared/assets")),
    RequestPath = "/shared/assets"
});
app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "../Shared/assets")),
    RequestPath = "/shared/assets"
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LCP Maui Web Api V1");
});

//app.UseCookiePolicy();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
//app.MapFallbackToPage("/_Host");
app.MapControllers();
app.MapFallbackToFile("mindex.html");

app.Run();
