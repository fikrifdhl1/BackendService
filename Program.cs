using BackendService.Middleware;
using BackendService.Models.Domain;
using BackendService.Models.DTO;
using BackendService.Repositories;
using BackendService.Repositories.DbContexts;
using BackendService.Services;
using BackendService.Utils;
using BackendService.Utils.Logger;
using BackendService.Utils.Mapper;
using BackendService.Validators.Product;
using BackendService.Validators.User;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region dbcontext
var sqlConnectionString = builder.Configuration["ConnectionString:SQL"];
builder.Services.AddDbContext<UserDbContex>(opt => opt.UseSqlServer(sqlConnectionString));
var mySqlConnectionString = builder.Configuration["ConnectionString:MySQL"];
builder.Services.AddDbContext<ProductDbContext>(opt => opt.UseMySql(mySqlConnectionString,ServerVersion.AutoDetect(mySqlConnectionString)));
var pgsqlConnectionString = builder.Configuration["ConnectionString:PgSQL"];
builder.Services.AddDbContext<TransactionDbContext>(opt => opt.UseNpgsql(pgsqlConnectionString));
#endregion

#region repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
#endregion

#region services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
#endregion

#region mapper
builder.Services.AddScoped<ICustomeMapper<User, UserDTO>, UserDTOMapper>();
#endregion

#region validator
builder.Services.AddScoped<IValidator<UserToCreateDTO>, UserRegisterDTOValidator>();
builder.Services.AddScoped<IValidator<UserToUpdateDTO>, UserUpdateDTOValidator>();
builder.Services.AddScoped<IValidator<CreateProductDTO>, CreateProductDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateProductDTO>, UpdateProductDTOValidator>();

builder.Services.AddScoped<IProductValidator, ProductValidator>();
builder.Services.AddScoped<IUserValidator, UserValidator>();
#endregion



builder.Services.AddSingleton<ICustomeLogger,ConsoleLogger>();
builder.Services.AddScoped<ICustomeHash, CustomeHash>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
