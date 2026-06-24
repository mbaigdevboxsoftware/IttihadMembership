using IttihadmembershipAPI.Business;
using IttihadmembershipAPI.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddOpenApi();

// Dependency Injection
builder.Services.AddScoped<IAuthenticationModel, AuthenticationModel>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUsersModel, UsersModel>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IWebsiteModel, WebsiteModel>();
builder.Services.AddScoped<IWebsiteService, WebsiteService>();
builder.Services.AddScoped<IMemberShipModel, MemberShipModel>();
builder.Services.AddScoped<IMemberShipService, MemberShipService>();
builder.Services.AddScoped<IPackageModel, PackageModel>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IRoleModel, RoleModel>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your token}"
    });


    options.AddSecurityRequirement(_ =>
    new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer"),
            new List<string>()
        }
    });
});
// JWT Authentication
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!))
        };
    });
const string AngularCorsPolicy = "AngularAdminPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(AngularCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Your explicit Angular URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Allows HttpOnly cookies to pass safely
    });
});

builder.Services.AddControllers();


var app = builder.Build();


// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ittihad Membership API V1");
        c.RoutePrefix = string.Empty; // Open Swagger at root URL
    });
}

app.UseHttpsRedirection();
app.UseCors("AngularAdminPolicy");

app.UseAuthentication(); // Must come before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();