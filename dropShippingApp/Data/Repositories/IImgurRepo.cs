using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface IImgurRepo
    {
        ImgurConfig GetConfig { get; }
        Task CreateInitialConfig(ImgurConfig config);
        Task UpdateConfig(ImgurConfig config);
    }
}
