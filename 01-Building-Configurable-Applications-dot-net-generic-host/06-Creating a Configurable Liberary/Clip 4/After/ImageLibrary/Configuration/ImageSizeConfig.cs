using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ImageLibrary.Configuration
{
    public class ImageSizeConfig
    {
        public const string Thumbnail = nameof(Thumbnail);
        public const string Medium = nameof(Medium);
        public const string Large = nameof(Large);

        [Range(32, 1024, ErrorMessage = "Width out of range")]
        public int Width { get; set; }
        public string FilePrefix { get; set; }
        public string WatermarkText { get;set; }
    }
}
