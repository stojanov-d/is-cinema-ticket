using IS_Domasna.Domain.DomainModels;
using IS_Domasna.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<TicketsInShoppingCart> TicketsInShoppingCart { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<Ticket>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<TicketsInShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<TicketsInShoppingCart>()
                .HasOne(z => z.Ticket)
                .WithMany(z => z.TicketsInShoppingCarts)
                .HasForeignKey(z => z.TicketId);

            builder.Entity<TicketsInShoppingCart>()
                .HasOne(z => z.ShoppingCart)
                .WithMany(z => z.TicketsInShoppingCarts)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<ShoppingCart>()
                .HasOne<ApplicationUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCart>(z => z.OwnerId);

            builder.Entity<TicketInOrder>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<TicketInOrder>()
                .HasOne(z => z.Ticket)
                .WithMany(z => z.TicketInOrders)
                .HasForeignKey(z => z.TicketId);

            builder.Entity<TicketInOrder>()
                .HasOne(z => z.Order)
                .WithMany(z => z.TicketInOrders)
                .HasForeignKey(z => z.OrderId);
        }
    }

        
}

