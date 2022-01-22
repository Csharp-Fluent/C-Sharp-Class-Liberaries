using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ImageLibrary.Configuration
{
    public class ValidateImageSizeConfig : IValidateOptions<ImageSizeConfig>
    {
        public ValidateOptionsResult Validate(string name, ImageSizeConfig options)
        {
            if(name == ImageSizeConfig.Thumbnail && options.Width > 96)
            {
                return ValidateOptionsResult.Fail("From Validator: Thumbnail should be 96px or smaller");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
