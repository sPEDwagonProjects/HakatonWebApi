using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TulaHackWebAPI.Etc;
using TulaHackWebAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication((opt) =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = Token.GetTokenValidationParameters();
                    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
          builder =>
          {
              builder.AllowAnyOrigin();
              builder.AllowAnyHeader();
                    



          });
});


var app = builder.Build();

app.UseCors("AllowAllHeaders");

// Configure the HTTP request pipeline.

app.UseSwagger();
    app.UseSwaggerUI();



//Add JWToken to all incoming HTTP Request Header
app.Use(async (context, next) =>
{
    var JWToken = context.Request.Query.FirstOrDefault(x => x.Key == "token").Value;
    if (!string.IsNullOrEmpty(JWToken))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
    }
    await next();
});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
