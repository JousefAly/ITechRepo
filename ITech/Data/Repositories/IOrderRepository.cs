using ITech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Repositories
{
    public interface IOrderRepository
    {
        //return created order Id
        int CreateOrder(Order order);
        List<Order> GetAllOrders(bool includeDetails = false);
        bool Accept(int orderId, string handlerId);
        bool Refuse(int orderId, string handlerId);
        Order GetById(int orderId, bool includeDetails = false);
        Order[] GetHandlerOrders(string handlerId, bool includeDetails = false);
        Order[] GetUserOrders(string userId, bool includeDetails = false);     
    }
}
