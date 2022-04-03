using WebApplicationCRUDExample.Services;

namespace WebApplicationCRUDExample;

public class Startup
{
   
        public Startup(IConfiguration configuration){ Configuration = configuration; }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(opts => opts.AddDefaultPolicy(dpo => dpo.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddSingleton<MongoDBContext>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
