using ITech.Models;
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
        public int CreateOrder(Order order)
        {
            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();

            if (!shoppingCartItems.Any())
                return 0;

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
    }
}
