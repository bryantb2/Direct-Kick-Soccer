using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealImgurRepo : IImgurRepo
    {
        // private fields
        private ApplicationDbContext context;

        public RealImgurRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ImgurConfig GetConfig
        {
            get
            {
                return this.context.ImgurConfiguration.ToList()[0];
            }
        }

        public async Task CreateInitialConfig(ImgurConfig config)
        {
            this.context.ImgurConfiguration.Add(config);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateConfig(ImgurConfig config)
        {
            this.context.ImgurConfiguration.Update(config);
            await this.context.SaveChangesAsync();
        }
    }
}
