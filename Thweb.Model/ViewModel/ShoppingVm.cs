using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thweb.Model.Model;

namespace Thweb.Model.ViewModel
{
    public class ShoppingVm
    {
        public OrderHeader orderHeader { get; set; }
        public OrderDetail orderDetail { get; set; }
    }
}
