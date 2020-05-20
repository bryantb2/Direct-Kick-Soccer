using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class RosterGroup
    {
        public int RosterGroupID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int ModelNumber { get; set; }
        [Required]
        public string GeneralThumbnail { get; set; }
        [Required]
        public ProductCategory Category { get; set; }
        public List<Tag> ProductTags { get; set; }
    }
}
