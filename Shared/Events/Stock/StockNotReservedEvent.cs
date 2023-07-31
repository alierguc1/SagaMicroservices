﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events.Stock
{
    public class StockNotReservedEvent
    {
        public Guid OrderId { get; set; }
        public string Message { get; set; }
    }
}