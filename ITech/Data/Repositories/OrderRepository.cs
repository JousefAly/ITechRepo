using ITech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly ApplicationDbContext _context;
        

        public OrderRepository(ShoppingCart shoppingCart,
                               ApplicationDbContext context)                               
        {
            _shoppingCart = shoppingCart;
            _context = context;
            
        }

        public bool Accept(int orderId, string handlerId)
        {
            
            var order = GetById(orderId);
            if (order == null)
                return false;
            order.Accepted = true;
            order.HandlerId = handlerId;
            order.OrderHandeled = DateTime.Now;
            return _context.SaveChanges() > 0;
        }

        public bool Refuse(int orderId, string handlerId)
        {
            var order = GetById(orderId);
            if (order == null)
                return false;
            order.Accepted = false;
            order.HandlerId = handlerId;
            order.OrderHandeled = DateTime.Now;
            return _context.SaveChanges() > 0;
        }

        public int CreateOrder(Order order)
        {
            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();

            if (!shoppingCartItems.Any())
                return 0;
            order.OrderDetails = new List<OrderDetail>();
            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                };

                order.OrderDetails.Add(orderDetail);
            }
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
            _context.Add(order);
            return _context.SaveChanges() > 0 ? order.OrderId : 0;


        }

        public List<Order> GetAllOrders(bool includeDetails = false)
        {
            if (includeDetails)
            {
               return  _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .ToList();
            }
            return _context.Orders.ToList();
        }
        public Order GetById(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return _context.Orders
                     .Include(o => o.User)
                     .Include(o => o.OrderDetails)
                     .ThenInclude(od => od.Product)
                     .FirstOrDefault(o => o.OrderId == id);
            }
            return _context.Orders.Find(id);
        }


    }
}
