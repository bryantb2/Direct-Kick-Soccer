using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories.RealRepos
{
    public class RealOrderRepo : IOrderRepo
    {
        private ApplicationDbContext context;

        public RealOrderRepo(ApplicationDbContext c)
        {
            this.context = c;
        }

        public List<Order> GetOrders
        {
            get
            {
                return this.context.Orders.ToList();
            }
        }

        public async Task AddOrder(Order order)
        {
            this.context.Orders.Add(order);
            await this.context.SaveChangesAsync();
        }
        
        public async Task<Order> RemoveOrder(int orderId)
        {
            var foundOrder = this.context.Orders.ToList()
                .Find(order => order.OrderID == orderId);
            this.context.Orders.Remove(foundOrder);
            await this.context.SaveChangesAsync();
            return foundOrder;
        }

        public async Task UpdateOrder(Order order)
        {
            this.context.Orders.Update(order);
            await this.context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            var foundOrder = this.context.Orders.ToList()
                .Find(order => order.OrderID == orderId);
            return foundOrder;
        }
    }
}
