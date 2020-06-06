﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.APIModels
{
    public class ImgurUpload
    {
        public IFormFile Image { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
