using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ImageLibrary.Configuration;

namespace ImageLibrary
{
    public interface IThumbnailProcessor
    {
        void ProcessImage(string imagePath);
    }

    public class ThumbnailProcessor : IThumbnailProcessor
    {
        private readonly ILogger<ThumbnailProcessor> _logger;
        private readonly ImageConfig _imageConfig;
        private ImageSizeConfig _thumbnailSizeConfig;

        public ThumbnailProcessor(ILogger<ThumbnailProcessor> logger,
            IOptions<ImageConfig> imageConfigOptions,
            IOptionsMonitor<ImageSizeConfig> imageSizeConfig)
        {
            _logger = logger;
            _imageConfig = imageConfigOptions.Value;
            _thumbnailSizeConfig = imageSizeConfig.Get(ImageSizeConfig.Thumbnail);

            
            imageSizeConfig.OnChange((thumbnailSizeConfig, name) =>
            {
                if (name == ImageSizeConfig.Thumbnail)
                {
                    _logger.LogInformation($"** Thumbnail Image Config Changed **");
                    _thumbnailSizeConfig = thumbnailSizeConfig;
                }
            });
        }

        public void ProcessImage(string imagePath)
        {
            _logger.LogInformation($"**** Processing: {imagePath} ****");
            _logger.LogInformation($"CompressionLevel: {_imageConfig.CompressionLevel}");
            _logger.LogInformation($"OutputPath: {_imageConfig.OutputPath}");
            _logger.LogInformation($"Thumbnail Width: {_thumbnailSizeConfig.Width}");
            _logger.LogInformation($"Thumbnail FilePrefix: {_thumbnailSizeConfig.FilePrefix}");
        }
    }
}
