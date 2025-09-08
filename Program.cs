using Microsoft.EntityFrameworkCore;
using pacient_manager.Data;
using patient_manager.Profiles;
using patient_manager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("Data Source=development.db");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(build =>
    build.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod());

app.UseHttpsRedirection();
app.MapControllers();
app.Run();