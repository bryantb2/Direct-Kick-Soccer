using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class Team
    {
        // private fields
        private List<CustomProduct> teamProducts = new List<CustomProduct>();
        private List<TeamTag> teamTags = new List<TeamTag>();

        // public properties
        public int TeamID { get; set; }
        public String TeamName { get; set; }
        public String TeamDescription { get; set; }
        public Country Country { get; set; }
        public String StreetAddress { get; set; }
        public int ZipCode { get; set; }
        public String CorporatePageURL { get; set; }
        public String BusinessEmail { get; set; }
        public int PhoneNumber { get; set; }
        public List<CustomProduct> TeamProducts { get { return teamProducts; } }
        public List<TeamTag> TeamTags { get { return teamTags; } }

        // methods
        public void AddTag(TeamTag tag) => teamTags.Add(tag);
        public TeamTag RemoveTag(TeamTag tag)
        {
            TeamTag removedTag = null;
            foreach (TeamTag t in teamTags)
            {
                if (t.TeamTagID == tag.TeamTagID)
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
