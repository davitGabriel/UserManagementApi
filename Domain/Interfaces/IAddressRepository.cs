using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAddressRepository : IGenericRepository<UserAddress>
    {
        IEnumerable<UserAddress> GetAddressByUserId(int userId);
    }
}
