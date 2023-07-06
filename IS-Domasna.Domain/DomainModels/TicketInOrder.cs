using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Domain.DomainModels
{
    public class TicketInOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int Quantity { get; set; }
    }
}
