using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ImageLibrary;
using ImageLibrary.Configuration;
using ImageLibrary.DependencyInjection;

namespace ImageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configBuilder) =>
                {
                    configBuilder.AddEnvironmentVariables(prefix: "ImageService_");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ImageFileWatcher>();
                    services.AddImageLibrary(hostContext.Configuration.GetSection(nameof(ImageConfig)),
                        new ImageConfig
                        {
                            CompressionLevel = 0.2M
                        },
                        thumbnailImageSizeConfig =>
                        {
                            thumbnailImageSizeConfig.FilePrefix = "th-";
                        });
                    
                    
                });
    }
}
