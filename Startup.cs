using JsonSubTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace DerivedTypesSerializationExample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
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
        }
    }
}
