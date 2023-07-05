using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Domain.DomainModels
{
    public class TicketsInShoppingCart : BaseEntity
    {
        public Guid TicketId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }

    }
}
