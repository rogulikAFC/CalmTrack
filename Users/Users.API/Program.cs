using Infrastructure.Persistence;
using System.Reflection;
using Users.Infrastructure.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(Assembly.Load("Users.Application")));

builder.Services.AddAuth(builder.Configuration);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
//            ValidAudience = builder.Configuration["JwtSettings:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Secret"]!)),
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//        });

//builder.Services.AddAuthorization();

//builder.Services.AddTransient<IAuthService, AuthService>();

//builder.Services
//    .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
