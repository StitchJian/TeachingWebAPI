using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using grade_sheet_api.Models;
using grade_sheet_api.Services;
using grade_sheet_api.IServices;

var builder = WebApplication.CreateBuilder(args);

// 設定檔
ConfigurationManager configuration = builder.Configuration;

// 決定db path
string? dbPath = configuration.GetValue<string>("environment") == "Y" ?
                configuration.GetConnectionString("Context") :
                configuration.GetConnectionString("ContextDev");
if (dbPath != null)
    builder.Services.AddDbContext<GradeContext>(options => options.UseSqlServer(dbPath));



// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "HCInternal API",
        Description = "",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});
// 跨域
builder.Services.AddCors(options =>
{
    options.AddPolicy("specificOrigins",
      policy =>
      {
          policy.WithOrigins(
                                "http://localhost:8080",
                                "https://localhost:5000"
                            )
                .AllowAnyHeader()
                .AllowAnyMethod(); ;
      }
      );
});

// automapper 註冊
builder.Services.AddAutoMapper(typeof(Program));

// service 註冊
builder.Services.AddScoped<IGradeService, GradeService>();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("specificOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
