using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageLibrary.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageLibrary.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddImageLibrary(this IServiceCollection services,
            IConfiguration configurationSection,
            ImageConfig defaultImageConfig,
            Action<ImageSizeConfig> configureThumbnailSize)
        {
            services.AddSingleton<IThumbnailProcessor, ThumbnailProcessor>();

            services.AddOptions<ImageConfig>()
                        .Configure(imageConfig =>
                        {
                            imageConfig.CompressionLevel = defaultImageConfig.CompressionLevel;
                        })
                        .Bind(configurationSection);

            services.AddOptions<ImageSizeConfig>(ImageSizeConfig.Thumbnail)
                .Configure(configureThumbnailSize)
                .Bind(configurationSection.GetSection(ImageSizeConfig.Thumbnail));


            services.Configure<ImageSizeConfig>(ImageSizeConfig.Medium, configurationSection.GetSection(ImageSizeConfig.Medium));
            services.Configure<ImageSizeConfig>(ImageSizeConfig.Large, configurationSection.GetSection(ImageSizeConfig.Large));

            return services;
        }
    }
}
