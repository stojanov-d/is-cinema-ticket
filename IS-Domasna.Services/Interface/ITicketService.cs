using IS_Domasna.Domain.DomainModels;
using IS_Domasna.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Services.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        Ticket GetDetailsForTicket(Guid id);
        void CreateNewTicket(Ticket ticket);
        void DeleteTicket(Guid id);
        void UpdateTicket(Ticket ticket);
        ShoppingCartDto GetShoppingCartInfo(Guid id);
        bool AddToShoppingCart(ShoppingCartDto item, string UserId);
    }
}
