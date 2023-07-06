using IS_Domasna.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }
    }
}
