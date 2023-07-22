using Order.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DataAccess.Repository.Interface
{
    public interface IOrderRepository
    {
        Task CreateOrder(OrderCreateDto orderCreateDto);
    }
}
