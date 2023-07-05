using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IS_Domasna.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        [Display(Name = "Movie Title")]
        [Required]
        public string MovieTitle { get; set; }
        [Display(Name = "Image link")]
        public string MovieImage { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string MovieDescription { get; set; }
        [Display(Name = "Date and time")]
        [Required]
        public DateTime MovieAirTime { get; set; }
        
        public ICollection<TicketsInShoppingCart> TicketsInShoppingCarts { get; set; }
    }
}
