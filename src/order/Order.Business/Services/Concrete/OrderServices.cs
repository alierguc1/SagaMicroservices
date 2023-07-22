using Order.Business.Services.Interfaces;
using Order.DataAccess.Repository.Interface;
using Order.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Business.Services.Concrete
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        public OrderServices(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task CreateOrder(OrderCreateDto orderCreateDto)
        {
            await _orderRepository.CreateOrder(orderCreateDto);
        }
    }
}
