using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Entity.Models
{

    [Owned]
    public class Adress
    {
        public string Line { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
    }
}
