using ITech.Data;
using ITech.Data.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Models
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _context;

        public string ShoppingCartId { get; set; }

        private ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();
            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        // return the shopping cart item with it is new state
        public ShoppingCartItem AddToCart(Product product, int amount = 1)
        {
            var shoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(i => i.Product.Id == product.Id && i.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = amount,
                };
                _context.Add(shoppingCartItem);
            }
            else
            {

                shoppingCartItem.Amount += amount;
            }
            _context.SaveChanges();
            return shoppingCartItem;
        }
        //return the remaining amount of a shopping cart item
        public int RemoveFromCart(Product product, int amount = 1)
        {
            var shoppingCartItem = _context.ShoppingCartItems
                 .FirstOrDefault(i => i.Product.Id == product.Id && i.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
                return 0;
            if (shoppingCartItem.Amount <= 1)
            {
                _context.Remove(shoppingCartItem);
                _context.SaveChanges();
                return 0;
            }
            shoppingCartItem.Amount -= amount;
            _context.SaveChanges();
            return shoppingCartItem.Amount;
        }
        
        public bool ClearCart()
        {
            var shoppingCartItems = _context.ShoppingCartItems
                .Where(i => i.ShoppingCartId == ShoppingCartId).ToList();

            _context.RemoveRange(shoppingCartItems);
            return _context.SaveChanges() > 0;
        }
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return _context.ShoppingCartItems
                .Where(i => i.ShoppingCartId == ShoppingCartId)
                .Include(i => i.Product).ToList();
        }
        public decimal GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems
                .Where(i => i.ShoppingCartId == ShoppingCartId)
                .Select(i => i.Amount * i.Product.PriceAfterDiscount).Sum();
        }

    }
}
