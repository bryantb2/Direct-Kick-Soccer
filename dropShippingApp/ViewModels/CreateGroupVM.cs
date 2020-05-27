using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
    public class CreateGroupVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string GeneralThumbnail { get; set; }
        [Required]
        public string PrintDesignPNG { get; set; }
        [Required]
        public int SelectedRosterGroupID { get; set; }
        public string NewTagName { get; set; }
        public int? ExistingTagID { get; set; }
        public List<Tag> DatabaseTags { get; set; }
        public List<RosterGroup> RosterGroups { get; set; }
    }
}
