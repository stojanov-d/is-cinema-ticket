using IS_Domasna.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Services.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteTicketFromSoppingCart(string userId, Guid ticketId);
        bool order(string userId);
    }
}
