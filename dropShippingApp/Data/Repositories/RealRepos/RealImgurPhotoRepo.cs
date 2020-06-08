using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealImgurPhotoRepo : IImgurPhotoRepo
    {
        // private fields
        private ApplicationDbContext context;

        public RealImgurPhotoRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<ImgurPhotoData> GetPhotos => this.context.SavedImgurPhotos.ToList();

        public async Task AddPhoto(ImgurPhotoData photo)
        {
            this.context.SavedImgurPhotos.Add(photo);
            await this.context.SaveChangesAsync();
        }

        public ImgurPhotoData GetByPhotoyId(string photoId)
        {
            // gets photo data by imgur id
            return this.context.SavedImgurPhotos.ToList()
                .Find(photo => photo.PhotoID == photoId);
        }

        public ImgurPhotoData GetByDBId(int id)
        {
            // gets photo data by database id
            return this.context.SavedImgurPhotos.ToList()
                .Find(photo => photo.ImgurPhotoDataID == id);
        }

        public async Task<ImgurPhotoData> RemovePhoto(int photoDBId)
        {
            // deletes database model by database id
            var foundPhoto = this.context.SavedImgurPhotos.ToList()
                .Find(photo => photo.ImgurPhotoDataID == photoDBId);
            this.context.SavedImgurPhotos.Remove(foundPhoto);
            await this.context.SaveChangesAsync();
            return foundPhoto;
        }

        public async Task UpdatePhoto(ImgurPhotoData updatedPhoto)
        {
            this.context.SavedImgurPhotos.Update(updatedPhoto);
            await this.context.SaveChangesAsync();
        }
    }
}
