using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftITOFlix.Data;
using Microsoft.AspNetCore.Identity;
using SoftITOFlix.Models;
namespace SoftITOFlix
{
    public class Program
    {
        SoftITOFlixContext _context;
        public Program(SoftITOFlixContext context)
        {
            _context = context;
        }
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SoftITOFlixContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SoftITOFlixContext") ?? throw new InvalidOperationException("Connection string 'SoftITOFlixContext' not found.")));

                        builder.Services.AddDefaultIdentity<SoftITOFlixUser>()
                .AddEntityFrameworkStores<SoftITOFlixContext>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();;

            app.UseAuthorization();

            SoftITOFlixContext? context = app.Services.CreateScope().ServiceProvider.GetService<SoftITOFlixContext>();

            context!.Database.Migrate();
            app.MapControllers();

            app.Run();
        }
    }
}
