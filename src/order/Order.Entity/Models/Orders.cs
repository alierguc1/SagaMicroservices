using Order.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Entity.Models
{
    public class Orders
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid BuyerId { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public OrderStatus Status { get; set; }
        public Adress Adress{ get; set; }
        public string FailMessage { get; set; }
        
    }
}
