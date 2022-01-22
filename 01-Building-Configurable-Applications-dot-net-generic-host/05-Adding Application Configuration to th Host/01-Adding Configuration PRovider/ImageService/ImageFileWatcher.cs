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

namespace ImageService
{
    public class ImageFileWatcher : IHostedService, IDisposable
    {
        private readonly ILogger<ImageFileWatcher> _logger;
        private readonly IConfiguration _configuration;
        private FileSystemWatcher _watcher;

        public ImageFileWatcher(ILogger<ImageFileWatcher> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Image File Watching: {_configuration["watchPath"]}");

            _watcher = new FileSystemWatcher(_configuration["watchPath"]);
            _watcher.Created += OnNewImage;
            _watcher.EnableRaisingEvents = true;

            return Task.CompletedTask;
        }

        private void OnNewImage(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"New Image: {e.FullPath}");
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
