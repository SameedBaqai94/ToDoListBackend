using Microsoft.EntityFrameworkCore;
using ToDoListBackend.Data;
using ToDoListBackend.Interfaces;
using ToDoListBackend.Repository;

namespace ToDoListBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IToDoListRepository, ToDoListRepository>();
            builder.Services.AddScoped<IItemsRepository, ItemsRepository>();
            builder.Services.AddAutoMapper(typeof(Program));
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dBContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                    dBContext.Database.Migrate();
                }
            }
            // Configure the HTTP request pipeline.

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
