using AngularWebApi.Data;
using AngularWebApi.Repositories;
using AngularWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// set env in PMC $env:ASPNETCORE_ENVIRONMENT='Local' for Local Migrations

//Defining DB Connection strings based on the environment
if (builder.Environment.IsEnvironment("Local"))
{
    // Code added for local development using localdb
    var connectionString = builder.Configuration.GetConnectionString("LocalDB");
    builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure()));
    DbContextOptionsBuilder<AppDBContext> ob = new DbContextOptionsBuilder<AppDBContext>();
    ob.UseSqlServer(connectionString);
    AppDBContext context = new AppDBContext(ob.Options);
    Console.WriteLine(context.Database.GetConnectionString());
    context.Database.GetPendingMigrations().ToList().ForEach(s => Console.WriteLine("pending : "+s));
    Console.WriteLine("\n");
    context.Database.Migrate();
    context.Database.GetAppliedMigrations().ToList().ForEach(s => Console.WriteLine("applied : " + s));

}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString));
}

//Add Repositories
builder.Services.AddScoped<RegionManager>();
builder.Services.AddScoped<RestaurentManager>();

//Add JWT Authentication
var JWTSetting = builder.Configuration.GetSection("JWTSetting");
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<TokenService>();
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(opt => {
        opt.SaveToken = true;
        opt.RequireHttpsMetadata = false;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = JWTSetting["ValidAudience"],
            ValidIssuer = JWTSetting["ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSetting.GetSection("securityKey").Value!))
        };
    });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyOrigins",
                          policy =>
                          {
                              policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                          });
});

builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization Example : 'Bearer eyeleieieekeieieie",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement(){
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "outh2",
                Name="Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("MyOrigins");

app.MapControllers();
app.Run();