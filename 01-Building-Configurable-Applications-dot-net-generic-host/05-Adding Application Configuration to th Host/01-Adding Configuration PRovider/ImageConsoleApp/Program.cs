using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ImageConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var switchMappings = new Dictionary<string, string>()
            {
                {"--thumbnailWidth", "thumbnail:width" },
                { "-cl", "compressionLevel" }
            };

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddCommandLine(args, switchMappings)
                .Build();
            
            Console.WriteLine("***** Process Image *****");
            Console.WriteLine($"Processing: {args[0]}");

            ImageConfig imageConfig = new ImageConfig()
            {
                CompressionLevel = 0.99M
            };
            configuration.GetSection(nameof(ImageConfig)).Bind(imageConfig);

            ProcessImage("Thumbnail", imageConfig.Thumbnail, imageConfig.CompressionLevel);
            ProcessImage("Medium", imageConfig.Medium, imageConfig.CompressionLevel);
            ProcessImage("Large", imageConfig.Large, imageConfig.CompressionLevel);

        }

        private static void ProcessImage(string imageSize, ImageSizeConfig config, decimal compressionLevel)
        {
            Console.WriteLine($"{imageSize} Width: {config.Width}");
            Console.WriteLine($"{imageSize} FilePrefix: {config.FilePrefix}");
            Console.WriteLine($"{imageSize} Watermark: {config.WatermarkText }");
            Console.WriteLine($"{imageSize} Compression Level: {compressionLevel}");
        }
    }
}
