

using System.Text.Json.Serialization;
using ExerciceWebApi.Middleware;
using ExerciceWebApi.Models.Entities;
using ExerciceWebApi.Services;
using ExerciceWebApi.Services.Gateway;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExerciceWebApi
{
    public class Startup{
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            //services.AddControllers();
            services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); //Elimina los ciclos infinitos de serializacion
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            
            services.AddScoped<IBaseCrudService<Product>, ProductService>();
            services.AddScoped<IBaseCrudService<Category>, CategoryService>();
            services.AddScoped<IBaseCrudService<InputOutput>, InputOutputService>();
            services.AddScoped<IBaseCrudService<Storage>, StorageService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IBaseCrudService<Warehouse>, WarehouseService>();

            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<DbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("webApiConnection")));

		}

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CatchExceptions>();
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
                        
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }

    }
}