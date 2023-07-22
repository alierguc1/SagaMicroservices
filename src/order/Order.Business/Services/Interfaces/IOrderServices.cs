using Order.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Business.Services.Interfaces
{
    public interface IOrderServices
    {
        Task CreateOrder(OrderCreateDto orderCreateDto);
    }
}
