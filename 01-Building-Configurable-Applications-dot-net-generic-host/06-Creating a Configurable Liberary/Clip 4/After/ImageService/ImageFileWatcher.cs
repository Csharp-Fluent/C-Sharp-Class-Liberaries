using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ImageLibrary;

namespace ImageService
{
    public class ImageFileWatcher : IHostedService, IDisposable
    {
        private readonly ILogger<ImageFileWatcher> _logger;
        private readonly IConfiguration _configuration;
        private readonly IThumbnailProcessor _thumbnailProcessor;
        private FileSystemWatcher _watcher;

        public ImageFileWatcher(ILogger<ImageFileWatcher> logger, IConfiguration configuration,
            IThumbnailProcessor thumbnailProcessor)
        {
            _logger = logger;
            _configuration = configuration;
            _thumbnailProcessor = thumbnailProcessor;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Image File Watching: {_configuration["watchPath"]}");

            _watcher = new FileSystemWatcher(_configuration["watchPath"], "*.jpg");

            _watcher.Created += OnNewImage;
            _watcher.EnableRaisingEvents = true;

            return Task.CompletedTask;
        }

        private void OnNewImage(object sender, FileSystemEventArgs e)
        {
            _thumbnailProcessor.ProcessImage(e.FullPath);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Image File Watching stopping");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("Disposing");
            _watcher.Dispose();
        }
    }
}
