using IS_Domasna.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Services.Interface
{
    public interface IOrderService
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(BaseEntity model);

        public Order GetById(Guid id);
    }
}
