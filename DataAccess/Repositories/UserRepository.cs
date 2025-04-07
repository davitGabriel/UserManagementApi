using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        public override User GetById(int id)
        {
            return _context.Set<User>()
                .Include(a => a.Addresses)
                .Include(g => g.GiftCards)
                .ToList()
                .Find(u => u.Id == id);
        }

        public override IEnumerable<User> GetAll()
        {
            return _context.Set<User>()
                .Include(a => a.Addresses)
                .Include(g => g.GiftCards)
                .ToList();
        }
    }
}
