using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Entity.Dtos
{
    public class OrderCreateDto
    {
        public Guid BuyerId { get; set; }
        public List<OrderItemDto> orderItems { get; set; }
        public PaymentDto payment { get; set; }
        public AdressDto adress { get; set; }
    }
}
