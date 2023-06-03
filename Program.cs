using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AttendanceSystemAPI.Data;


var CORSAllowSpecificOrigins = "_CORSAllowed";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AttendanceSystemAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AttendanceSystemAPIContext") ?? throw new InvalidOperationException("Connection string 'AttendanceSystemAPIContext' not found.")));

//CORS HERE
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORSAllowSpecificOrigins,
    policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod();
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000");
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors(CORSAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
