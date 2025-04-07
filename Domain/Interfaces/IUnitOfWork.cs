using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IAddressRepository Addresses { get; }
        IGiftCardRepository GiftCards { get; }

        //IGenericRepository<User> UserRepository{ get; }
        //IGenericRepository<UserAddress> AddressRepository { get; }
        //IGenericRepository<UserGiftCard> GiftCardRepository { get; }
        int Complete();
    }
}
