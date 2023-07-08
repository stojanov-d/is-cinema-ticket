using IS_Domasna.Domain.DomainModels;
using IS_Domasna.Repository.Interface;
using IS_Domasna.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return this._orderRepository.getOrderDetails(model);
        }
    }
}
