using AngularWebApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("MyOrigins");

app.MapControllers();
app.Run();