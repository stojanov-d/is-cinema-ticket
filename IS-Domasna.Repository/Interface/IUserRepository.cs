using IS_Domasna.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Domasna.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAll();

        ApplicationUser GetById(string id);

        void Insert(ApplicationUser user);

        void Update(ApplicationUser user);
        void Delete(ApplicationUser user);
    }
}
