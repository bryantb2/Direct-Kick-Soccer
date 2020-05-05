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
        private List<CustomProduct> teamProducts = new List<CustomProduct>();
        private List<Tag> teamTags = new List<Tag>();

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
        public List<CustomProduct> TeamProducts { get { return teamProducts; } }
        public List<Tag> TeamTags { get { return teamTags; } }
        [Required]
        public bool IsTeamInactive { get; set; }
        [Required]
        public bool IsHostTeam { get; set; }

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

        public void AddProduct(CustomProduct product) => teamProducts.Add(product);
        public CustomProduct RemoveProduct(CustomProduct product)
        {
            CustomProduct removedProduct = null;
            foreach (CustomProduct p in teamProducts)
            {
                if (product.CustomProductID == p.CustomProductID)
                {
                    removedProduct = p;
                    teamProducts.Remove(p);
                    return removedProduct;
                }
            }
            return removedProduct;
        }

        
    }
}
