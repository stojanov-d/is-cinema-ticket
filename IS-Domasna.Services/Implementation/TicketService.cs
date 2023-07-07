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
        private readonly IUserRepository _userRepository;
        private readonly IRepository<TicketsInShoppingCart> ticketInShoppingCartRepository;

        public TicketService(IRepository<Ticket> ticketRepository, IUserRepository _userRepository , IRepository<TicketsInShoppingCart> ticketInShoppingCartRepository)
        {
            _ticketRepository = ticketRepository;
            this._userRepository = _userRepository;
            this.ticketInShoppingCartRepository = ticketInShoppingCartRepository;
        }
        public bool AddToShoppingCart(AddToShoppingCardDto item, string UserId)
        {
            var user = this._userRepository.GetById(UserId);

            var userShoppingCard = user.UserCart;

            var selectedTicket = _ticketRepository.Get(item.SelectedTicketId);

            if (selectedTicket != null && userShoppingCard != null)
            {
                var product = this.GetDetailsForTicket(item.SelectedTicketId);
                //{896c1325-a1bb-4595-92d8-08da077402fc}

                if (product != null)
                {
                    TicketsInShoppingCart itemToAdd = new TicketsInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Ticket = product,
                        TicketId = product.Id,
                        ShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    var existing = userShoppingCard.TicketsInShoppingCarts.Where(z => z.ShoppingCartId == userShoppingCard.Id && z.TicketId == itemToAdd.TicketId).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.Quantity += itemToAdd.Quantity;
                        this.ticketInShoppingCartRepository.Update(existing);

                    }
                    else
                    {
                        this.ticketInShoppingCartRepository.Insert(itemToAdd);
                    }
                    return true;
                }
                return false;
            }
            return false;
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

        public AddToShoppingCardDto GetShoppingCartInfo(Guid id)
        {
            var product = this.GetDetailsForTicket(id);
            AddToShoppingCardDto model = new AddToShoppingCardDto
            {
                SelectedTicket = product,
                SelectedTicketId = product.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdateTicket(Ticket ticket)
        {
            _ticketRepository.Update(ticket);
        }
    }
}
