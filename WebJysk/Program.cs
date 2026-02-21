using System.ComponentModel;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<EmailSetting>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services
    .AddIdentityCore<User>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;

        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();


var jwt = builder.Configuration.GetSection("Jwt");
var keyBytes = Encoding.UTF8.GetBytes(jwt["Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SendEmailPolicy",
        policy => policy.RequireClaim("Permission", "SendEmail"));
});

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHostedService<EmailWorker>();
builder.Services.AddLogging(config => { config.AddConsole(); });

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("ReportJob");

    q.AddJob<ReportJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithSimpleSchedule(x =>
            x.WithIntervalInMinutes(2)
             .RepeatForever()));
});

builder.Services.AddQuartzHostedService();

builder.Services.AddScoped<IUserService,UserService>();
// builder.Services.AddScoped<IOrderService,OrderService>();
// builder.Services.AddScoped<IProductService,ProductService>();
// builder.Services.AddScoped<IWarhouseService, WarhouseServce>();
builder.Services.AddScoped<ICategoryService, CategoryServce>();
builder.Services.AddScoped<ICartService,CartService>(); 
builder.Services.AddScoped<IBrandService,BrandService>();
builder.Services.AddScoped<IDeliveryService,DeliveryService>();
builder.Services.AddScoped<IPaymentService,PaymentService>();
builder.Services.AddScoped<IDescountService,DescountService>();
builder.Services.AddScoped<IReviewService,ReviewService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Р’РІРµРґРёС‚Рµ JWT С‚РѕРєРµРЅ С‚Р°Рє: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

try
{
    await using var scope = app.Services.CreateAsyncScope();
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await Roles.SeedRoles(roleManager);

    app.Logger.LogInformation("Finished Seeding Default Data");
    app.Logger.LogInformation("Application Starting");
}
catch (Exception ex)
{
    app.Logger.LogError("An Error occurred while seeding the db:  {ExMessage}", ex.Message);
}


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
