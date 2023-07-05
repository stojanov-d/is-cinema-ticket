using IS_Domasna.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketsInShoppingCart> Tickets { get; set; }

        public double TotalPrice { get; set; }
    }
}
