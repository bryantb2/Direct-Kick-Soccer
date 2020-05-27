using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class UpdateGroupVM
    {
        public int GroupID { get; set; }
        [Required]
        [MinLength(10)]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        public string Description { get; set; }
        [Required]
        public string ThumbnailURL { get; set; }
        public string NewTagName { get; set; }
        public int? ExistingTagID { get; set; }
        public int? RemovedTagID { get; set; }
        public List<Tag> DatabaseTags { get; set; }
        public List<Tag> ExistingGroupTags { get; set; }
    }
}
