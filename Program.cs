using BackendService.Middleware;
using BackendService.Models.Domain;
using BackendService.Models.DTO;
using BackendService.Repositories;
using BackendService.Repositories.DbContexts;
using BackendService.Services;
using BackendService.Utils;
using BackendService.Utils.Logger;
using BackendService.Validators.Cart;
using BackendService.Validators.Product;
using BackendService.Validators.Transaction;
using BackendService.Validators.User;
using BPKBBackend.Models;
using BPKBBackend.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
builder.Services.AddDbContext<CartDbContext>(opt => opt.UseNpgsql(pgsqlConnectionString));
#endregion

#region repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
#endregion

#region services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
#endregion


#region validator
builder.Services.AddScoped<IValidator<UserToCreateDTO>, UserRegisterDTOValidator>();
builder.Services.AddScoped<IValidator<UserToUpdateDTO>, UserUpdateDTOValidator>();
builder.Services.AddScoped<IValidator<CreateProductDTO>, CreateProductDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateProductDTO>, UpdateProductDTOValidator>();
builder.Services.AddScoped<IValidator<CreateCartDTO>, CreateCartDTOValidator>();
builder.Services.AddScoped<IValidator<CreateCartItemDTO>, CreateCartItemDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateCartItemDTO>, UpdateCartItemValidator>();
builder.Services.AddScoped<IValidator<CreateTransactionDTO>, CreateTransactionDTOValidator>();
builder.Services.AddScoped<IValidator<LoginRequestDTO>, UserLoginDTOValidator>();


builder.Services.AddScoped<IProductValidator, ProductValidator>();
builder.Services.AddScoped<IUserValidator, UserValidator>();
builder.Services.AddScoped<ICartValidator, CartValidator>();
builder.Services.AddScoped<ITransactionValidator, TransactionValidator>();
#endregion

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddScoped<IJwtHelper, JwtHelper>();
builder.Services.AddSingleton<ICustomeLogger,ConsoleLogger>();
builder.Services.AddScoped<ICustomeHash, CustomeHash>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                          Enter 'Bearer' [space] and then your token in the text input below.
                          Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
