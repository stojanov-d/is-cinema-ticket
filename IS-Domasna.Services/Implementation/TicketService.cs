using IS_Domasna.Domain.DomainModels;
using IS_Domasna.Domain.DTO;
using IS_Domasna.Repository.Interface;
using IS_Domasna.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS_Domasna.Services.Implementation
{
    public class TicketService : ITicketService
    {

        public readonly IRepository<Ticket> _ticketRepository;

        public TicketService(IRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public bool AddToShoppingCart(ShoppingCartDto item, string UserId)
        {
            throw new NotImplementedException();
        }

        public void CreateNewTicket(Ticket ticket)
        {
            this._ticketRepository.Insert(ticket);
        }

        public void DeleteTicket(Guid id)
        {
            var ticket = _ticketRepository.Get(id);
            _ticketRepository.Delete(ticket);
        }

        public List<Ticket> GetAllTickets()
        {
            return _ticketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTicket(Guid id)
        {
            return this._ticketRepository.Get(id);
        }

        public ShoppingCartDto GetShoppingCartInfo(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTicket(Ticket ticket)
        {
            _ticketRepository.Update(ticket);
        }
    }
}
