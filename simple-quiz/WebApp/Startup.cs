using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using BLL.App;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF;
using Domain.App.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace WebApp
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection"))
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging());

            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IAppBLL, AppBLL>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication()
                .AddCookie(options => { options.SlidingExpiration = true; }
                )
                .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidIssuer = Configuration["JWT:Issuer"],
                            ValidAudience = Configuration["JWT:Issuer"],

                            IssuerSigningKey =
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                            ClockSkew = TimeSpan.Zero
                        };
                    }
                );

            services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });

            services.AddAutoMapper(
                typeof(DAL.App.DTO.MappingProfiles.AutoMapperProfile),
                typeof(BLL.App.DTO.MappingProfiles.AutoMapperProfile),
                typeof(PublicApi.DTO.v1.MappingProfiles.AutoMapperProfile)
            );

            services.AddApiVersioning(options => { options.ReportApiVersions = true; });
            services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseCors("CorsAllowAll");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            SetupAppData(app);
        }
        
        private static async Task SetupAppData(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            await using var ctx = serviceScope.ServiceProvider.GetService<AppDbContext>();
            if (ctx == null) return;
            if (ctx!.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
                return;

            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

            var roleExists = await roleManager!.RoleExistsAsync("Admin");

            if (roleExists) return;

            var role = new AppRole {Name = "Admin"};
            await roleManager!.CreateAsync(role);

            var user = new AppUser {Email = "admin@admin.com", UserName = "admin"};

            var userRes = await userManager!.CreateAsync(user, "Admin123.");
            if (userRes.Succeeded) await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}