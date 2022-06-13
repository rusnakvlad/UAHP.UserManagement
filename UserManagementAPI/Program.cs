using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManagement.BLL.IServices;
using UserManagement.BLL.JWT;
using UserManagement.BLL.Services;
using UserManagement.DAL.EF;
using UserManagement.DAL.Enteties;
using UserManagement.DAL.IRepositories;
using UserManagement.DAL.Repositories;
using UserManagement.DAL.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region REPOSITORIES
builder.Services.AddTransient<IUserRepository, UserRepository>();
#endregion

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

#region SERVICES
builder.Services.AddTransient<IUserService, UserService>();
#endregion

#region AUTOMAPPER
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString:connectDB"]));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services
// Adding Authentication
.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding JWT Bearer
.AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = JwtOptions.AUDIENCE,
            ValidIssuer = JwtOptions.ISSUER,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.KEY)),
            ValidateLifetime = true,
            LifetimeValidator = JwtOptions.ValidateLifeTime
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
