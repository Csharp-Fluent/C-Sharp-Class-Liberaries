using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageService.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                    services.AddSingleton<IThumbnailProcessor, ThumbnailProcessor>();

                    var config = hostContext.Configuration;
                    
                    services.AddOptions<ImageConfig>()
                        .Configure(imageConfig =>
                        {
                            imageConfig.CompressionLevel = 0.99M;
                        })
                        .Bind(config.GetSection(nameof(ImageConfig)));

                    services.AddOptions<ImageSizeConfig>(ImageSizeConfig.Thumbnail)
                        .Configure(thumbnailSizeConfig =>
                        {
                            thumbnailSizeConfig.FilePrefix = "thumb-";
                        })
                        .Bind(config.GetSection("ImageConfig:thumbnail"));

                    
                    services.Configure<ImageSizeConfig>(ImageSizeConfig.Medium, config.GetSection("ImageConfig:medium"));
                    services.Configure<ImageSizeConfig>(ImageSizeConfig.Large, config.GetSection("ImageConfig:large"));
                });
    }
}
