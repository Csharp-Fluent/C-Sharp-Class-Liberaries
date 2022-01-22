using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Configuration
{
    public class ImageSizeConfig
    {
        public const string Thumbnail = nameof(Thumbnail);
        public const string Medium = nameof(Medium);
        public const string Large = nameof(Large);

        public int Width { get; set; }
        public string FilePrefix { get; set; }
        public string WatermarkText { get;set; }
    }
}
