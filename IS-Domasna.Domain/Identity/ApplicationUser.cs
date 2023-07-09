using IS_Domasna.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ShoppingCart UserCart { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
