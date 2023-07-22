using Shared.Messages.order;
using Shared.Messages.payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events.order
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public Guid BuyerId { get; set; }
        public PaymentMessage Payment { get; set; }
        public List<OrderItemMessage> OrderItem { get; set; } = new List<OrderItemMessage>();
    }
}
