using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class Team
    {
        // private fields
        private List<Tag> teamTags = new List<Tag>();
        private List<ProductGroup> productGroups = new List<ProductGroup>();

        // public properties
        public int TeamID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public Country Country { get; set; }
        [Required]
        public Province Providence { get; set; }
        [Required]
        public String StreetAddress { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public String CorporatePageURL { get; set; }
        [Required]
        public String BusinessEmail { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public List<Tag> TeamTags { get { return teamTags; } }
        [Required]
        public bool IsTeamInactive { get; set; }
        [Required]
        public bool IsHostTeam { get; set; }
        [Required]
        public DateTime DateJoined { get; set; }
        [Required]
        public Category Category { get; set; }
        public List<ProductGroup> ProductGroups { get; set; }

        // methods
        public void AddTag(Tag tag) => teamTags.Add(tag);
        public Tag RemoveTag(Tag tag)
        {
            Tag removedTag = null;
            foreach (Tag t in teamTags)
            {
                if (t.TagID == tag.TagID)
                {
                    removedTag = t;
                    teamTags.Remove(t);
                    return removedTag;
                }
            }
            return removedTag;
        }

        public void AddProductGroup(ProductGroup group) => productGroups.Add(group);
        public void RemoveProductGroup(int groupId) => productGroups.Remove(productGroups.Find(group => group.ProductGroupID == groupId));
        public void UpdateProductGroup(ProductGroup updatedGroup) => 
            productGroups[productGroups.FindIndex(group => group.ProductGroupID == updatedGroup.ProductGroupID)] = updatedGroup;
    }
}
