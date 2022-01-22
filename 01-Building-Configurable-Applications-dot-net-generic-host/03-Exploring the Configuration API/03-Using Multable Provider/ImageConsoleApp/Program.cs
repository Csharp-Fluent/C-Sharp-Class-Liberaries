using System;
using Microsoft.Extensions.Configuration;

namespace ImageConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddCommandLine(args)
                .Build();
            
            Console.WriteLine("***** Process Image *****");
            Console.WriteLine($"Processing: {args[0]}");

            Console.WriteLine($"Thumbnail Width: {configuration["THUMBNAILWIDTH"]}");
            Console.WriteLine($"Thumbnail FilePrefix: {configuration["thumbnailFilePrefix"]}");

            Console.WriteLine($"Medium Width: {configuration["mediumWidth"]}");
            Console.WriteLine($"Medium FilePrefix: {configuration["mediumFilePrefix"]}");

            Console.WriteLine($"Large Width: {configuration["largeWidth"]}");
            Console.WriteLine($"Large FilePrefix: {configuration["largeFilePrefix"]}");

            Console.WriteLine($"Watermark: {configuration["watermarkText"] }");

            Console.WriteLine($"Compression Level: {configuration["compressionLevel"] }");
        }
    }
}
