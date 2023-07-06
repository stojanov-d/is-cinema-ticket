using IS_Domasna.Domain.Identity;
using IS_Domasna.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS_Domasna.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<ApplicationUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<ApplicationUser>();
        }

        public void Delete(ApplicationUser user)
        {
            if(user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            entities.Remove(user);
            context.SaveChanges();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public ApplicationUser GetById(string id)
        {
            return entities
                .Include(e => e.UserCart)
                .Include("UserCart.TicketsInShoppingCarts")
                .Include("UserCart.TicketsInShoppingCarts.Ticket")
                .SingleOrDefault(e => e.Id  == id);
        }

        public void Insert(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            entities.Add(user);
            context.SaveChanges();
        }

        public void Update(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            entities.Update(user);
            context.SaveChanges();
        }
    }
}
