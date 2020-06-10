using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Data.Repositories
{
    public interface IOrderRepo
    {
        // order methods
        Task AddOrder(Order order);
        Task<Order> RemoveOrder(int orderId);
        Task UpdateOrder(Order order);
        Task<Order> GetOrderById(int orderId);
        List<Order> GetOrders { get; }

        // order item methods
        Task AddOrderItem(OrderItem item);
        Task<OrderItem> RemoveOrderItem(int itemId);
        Task UpdateOrderItem(OrderItem item);
    }
}