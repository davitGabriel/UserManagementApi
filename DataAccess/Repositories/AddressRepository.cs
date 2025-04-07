using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class AddressRepository : GenericRepository<UserAddress>, IAddressRepository
    {
        public AddressRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<UserAddress> GetAddressByUserId(int userId)
        {
            return _context.Set<UserAddress>().Where(a => a.UserId == userId);
        }
    }
}
