using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.APIModels
{
    public class ImgurUploadResponse
    {
        public ImgurSubDataResponse data { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
    }
}
