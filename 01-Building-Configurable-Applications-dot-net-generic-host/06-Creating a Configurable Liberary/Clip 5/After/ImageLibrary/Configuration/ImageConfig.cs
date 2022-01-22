using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ImageLibrary.Configuration
{
    public class ImageConfig
    {
        [Range(0, 1, ErrorMessage = "Compression must be 0 to 1")]
        public decimal CompressionLevel { get; set; }
        public string OutputPath { get; set; }
    }
}
