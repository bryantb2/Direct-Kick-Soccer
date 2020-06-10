using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface IImgurPhotoRepo
    {
        List<ImgurPhotoData> GetPhotos { get; }
        ImgurPhotoData GetByPhotoyId(string photoId);
        ImgurPhotoData GetByDBId(int id);
        Task UpdatePhoto(ImgurPhotoData updatedPhoto);
        Task AddPhoto(ImgurPhotoData updatedPhoto);
        Task<ImgurPhotoData> RemovePhoto(int photoId);
    }
}
