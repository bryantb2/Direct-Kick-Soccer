﻿using System;
using System.Collections.Generic;
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
        public String TeamName { get; set; }
        public String TeamDescription { get; set; }
        public Country Country { get; set; }
        public Province Providence { get; set; }
        public String StreetAddress { get; set; }
        public int ZipCode { get; set; }
        public String CorporatePageURL { get; set; }
        public String BusinessEmail { get; set; }
        public int PhoneNumber { get; set; }
        public List<CustomProduct> TeamProducts { get { return teamProducts; } }
        public List<Tag> TeamTags { get { return teamTags; } }
        public bool IsTeamInactive { get; set; }
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
