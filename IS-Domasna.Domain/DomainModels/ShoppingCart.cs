using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public IdentityUser Owner { get; set; }

        public ICollection<TicketsInShoppingCart> TicketsInShoppingCarts { get; set; }

    }
}
