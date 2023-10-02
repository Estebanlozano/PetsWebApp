using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetsWebApp.Application.Services;
using PetsWebApp.Application.Validators;
using PetsWebApp.Core.Interfaces.Repositories;
using PetsWebApp.Core.Interfaces.Services;
using PetsWebApp.Core.Interfaces.Validators;
using PetsWebApp.Infrastructure.Data;
using PetsWebApp.Infrastructure.Repositories;
using PetsWebApp.Infrastructure.Data.Repositories;
using AutoMapper;
using PetsWebApp.Core.Entities;
using PetsWebApp.Web.ViewModels;

namespace PetsWebApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("PetsWebApp.Web")));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            builder.Services.AddScoped<IUserProfileService, UserProfileService>();
            builder.Services.AddScoped<IUserProfileValidator, UserProfileValidator>();
            builder.Services.AddScoped<IPetRepository, PetRepository>();
            builder.Services.AddScoped<IPetValidator, PetValidator>();
            builder.Services.AddScoped<IPetService, PetService>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IPaginationService, PaginationService>();
            builder.Services.AddScoped<IRequestAdoptionRepository, RequestAdoptionRepository>();
            builder.Services.AddScoped<IRequestAdoptionService, RequestAdoptionService>();
            builder.Services.AddScoped<IRequestAdoptionValidator, RequestAdoptionValidator>();
            RegisterMapper(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Pet}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        private static void RegisterMapper(WebApplicationBuilder builder)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PetViewModel, Pet>()
                    .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos));

                cfg.CreateMap<Pet, PetViewModel>()
                    .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos));

                cfg.CreateMap<string, PetPhoto2>()
                    .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src));

                cfg.CreateMap<PetPhoto2, string>()
                    .ConvertUsing(x => x.Path ?? string.Empty);

                cfg.CreateMap<RequestAdoption, RequestAdoptionViewModel>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();

            builder.Services.AddSingleton(mapper);
        }
    }
}