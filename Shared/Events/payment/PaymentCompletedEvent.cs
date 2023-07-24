using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events.payment
{
    public class PaymentCompletedEvent
    {
        public Guid OrderId { get; set; }
        public Guid BuyerId { get; set; }
    }
}
