using machineConfig.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO.Compression;
using WebMarkupMin.AspNet.Common.Compressors;
using WebMarkupMin.AspNetCore2;

namespace machineConfig
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        public Startup(IHostingEnvironment env)
        {
            Configuration =
                new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddWebMarkupMin(opt => opt.AllowCompressionInDevelopmentEnvironment = true)
                .AddXmlMinification()
                .AddHtmlMinification()
                .AddHttpCompression(options => options.CompressorFactories = new[] {
                        new GZipCompressorFactory(new GZipCompressionSettings { Level = CompressionLevel.Fastest })});

            services.AddAntiforgery();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseWebMarkupMin();
            app.UseMvc(routeBuilder => Routing.ConfigureRoutes(routeBuilder));
        }
    }
}