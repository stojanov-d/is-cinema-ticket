using IS_Domasna.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(BaseEntity model);

        public Order getOrder(Guid id);

    }
}
