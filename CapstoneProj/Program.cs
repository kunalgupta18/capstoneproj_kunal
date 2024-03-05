
using CapstoneProj.Repository;
using CapstoneProj.Repository.Interface;
using CapstoneProj.Services;
using CapstoneProj.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    }
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddScoped<ISupplierRepo, SupplierRepo>();
builder.Services.AddTransient<ISupplierService, SupplierService>();

builder.Services.AddScoped<IPurchaseRepo, PurchaseRepo>();
builder.Services.AddTransient<IPurchaseService, PurchaseService>();

builder.Services.AddScoped<ISalesRepo, SalesRepo>();
builder.Services.AddTransient<ISalesService, SalesService>();

builder.Services.AddScoped<IDisplayRepo, DisplayRepo>();
builder.Services.AddTransient<IDisplayService, DisplayService>();

builder.Services.AddScoped<IClientRepo, ClientRepo>();
builder.Services.AddTransient<IClientService, ClientService>();

builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddCors((o) =>
{
    o.AddPolicy("corspolicy", b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("corspolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
