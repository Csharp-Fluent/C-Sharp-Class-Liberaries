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

            Console.WriteLine($"Thumbnail Width: {configuration["THUMBNAIL:WIDTH"]}");
            Console.WriteLine($"Thumbnail FilePrefix: {configuration["thumbnail:FilePrefix"]}");

            Console.WriteLine($"Medium Width: {configuration["medium:Width"]}");
            Console.WriteLine($"Medium FilePrefix: {configuration["medium:FilePrefix"]}");

            Console.WriteLine($"Large Width: {configuration["large:Width"]}");
            Console.WriteLine($"Large FilePrefix: {configuration["large:FilePrefix"]}");

            Console.WriteLine($"Watermark: {configuration["watermarkText"] }");

            Console.WriteLine($"Compression Level: {configuration["compressionLevel"] }");
        }
    }
}
