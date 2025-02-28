using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebAPIProject.Business_Logic.Map;
using WebAPIProject.Business_Logic.Services;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Contract.Service;
using WebAPIProject.Infrastructure.Data;
using WebAPIProject.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });


builder.Services.AddScoped<ICompanyRepository, CompanyRepository>(); 
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<CompanyMapper>();    
builder.Services.AddScoped<IDyeStuffService, DyeStuffService>();
builder.Services.AddScoped<IDyeStuffRepository, DyeStuffRepository>();
builder.Services.AddScoped<IAPIUserContext, APIUserContext>();
builder.Services.AddSingleton<DyeStuffMapper>();
builder.Services.AddScoped<DyeStuffMapper>();

builder.Services.AddTransient<IFabricService, FabricService>();
builder.Services.AddTransient<IFabricRepository, FabricRepository>();
builder.Services.AddScoped<FabricMapper>();
builder.Services.AddScoped<IEmailRepository,EmailRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAPIUserContext,APIUserContext>();


builder.Services.AddScoped<ISymbolCategoryRepository, SymbolCategoryRepository>();
builder.Services.AddScoped<ISymbolCategoryService, SymbolCategoryService>();
builder.Services.AddScoped<SymbolCategoryMapper>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IDyeTypeRepository, DyeTypeRepository>();
builder.Services.AddScoped<IDyeTypeService, DyeTypeService>();
builder.Services.AddScoped<DyeTypeMapper>();

builder.Services.AddControllers();
;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,    
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = builder.Configuration["Jwt:Issuer"],
                   ValidAudience = builder.Configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
               };
           });

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your token in the text input below.\n\nExample: Bearer 12345abcdef"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
