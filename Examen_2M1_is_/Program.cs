
using AutoMapper;
using Examen_2M1_is_.DAta;
using Examen_2M1_is_.Repository;
using Examen_2M1_is_.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Examen_2M1_is_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<HotelContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(typeof(mapeador));
            builder.Services.AddScoped<IHabitacionesRepository, HabitacionesRepository>();

            builder.Services.AddScoped<IReservasRepository, ReservasRepository>();



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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
