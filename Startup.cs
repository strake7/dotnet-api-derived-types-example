using JsonSubTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace DerivedTypesSerializationExample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(
                        JsonSubtypesConverterBuilder
                        .Of(typeof(Musician), nameof(Musician.InstrumentType))
                        .RegisterSubtype(typeof(Musician.Guitarist), "Guitar")
                        .RegisterSubtype(typeof(Musician.Pianist), "Piano")
                        .SerializeDiscriminatorProperty()
                        .Build()
                    );
                });

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.GeneratePolymorphicSchemas(discriminatorSelector: d => {
                    if (d == typeof(Musician))
                        return "instrumentType";
                    return null;
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
            app.UseEndpoints(e => e.MapControllers());

            app.UseSwagger();
            app.UseSwaggerUI(c=>{
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
