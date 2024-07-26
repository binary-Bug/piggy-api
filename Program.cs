var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
app.Logger.LogInformation("\n*************************************************************\n");
app.Logger.LogCritical("\nwebapi starting\n");
app.Logger.LogInformation("\n*************************************************************\n");
app.Run();
app.Logger.LogInformation("\n*************************************************************\n");
app.Logger.LogInformation("\nwebapi started running succesfully\n");
app.Logger.LogInformation("\n*************************************************************\n");
