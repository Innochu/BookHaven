using BookHaven.Messaging;
using Microsoft.OpenApi.Models;

namespace BookHaven.Configurations
{
    public class SwaggerExtension
    {
        public void ConfigureServices(IServiceCollection services)
        {
           

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookHaven API", Version = "v1" });
                c.OperationFilter<SwaggerDefaultValues>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
         

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookHaven API V1");
                c.RoutePrefix = string.Empty;
            });

            var inventoryUpdater = app.ApplicationServices.GetRequiredService<InventoryUpdater>();
            inventoryUpdater.StartListening();
        }
    }
}
