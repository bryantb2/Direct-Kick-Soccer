using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class TeamManager
    {
        // private fields
        private List<CustomProduct> createdCustomProducts = new List<CustomProduct>();
        private List<ActivityLog> activityLog = new List<ActivityLog>();

        // public properties
        public int TeamManagerID { get; set; }
        public Team ManagedTeam { get; set; }
        public AppUser BaseUser { get; set; }
        public List<CustomProduct> CreatedCustomProducts { get { return createdCustomProducts; } }
        public List<ActivityLog> ActivityLog { get { return activityLog; } }

        // methods
        public void AddCustomProduct(CustomProduct product) => createdCustomProducts.Add(product);
        public CustomProduct RemoveCustomProduct(CustomProduct product)
        {
            CustomProduct removedProduct = null;
            foreach(CustomProduct p in createdCustomProducts)
            {
                if(p.CustomProductID == product.CustomProductID)
                {
                    removedProduct = p;
                    createdCustomProducts.Remove(p);
                    return removedProduct;
                }
            }
            return removedProduct;
        }

        public void AddLog(ActivityLog log) => activityLog.Add(log);
        public ActivityLog RemoveLog(ActivityLog log)
        {
            ActivityLog removedLog = null;
            foreach(ActivityLog l in activityLog)
            {
                if(l.ActivityLogID == log.ActivityLogID)
                {
                    removedLog = l;
                    activityLog.Remove(l);
                    return removedLog;
                }
            }
            return removedLog;
        }
    }
}
