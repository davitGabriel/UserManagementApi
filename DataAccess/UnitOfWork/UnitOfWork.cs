using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext applicationContext, 
            IUserRepository userRepository,
            IAddressRepository addressRepository,
            IGiftCardRepository giftCardRepository)
        {
            _context = applicationContext;

            Users = userRepository;
            Addresses = addressRepository;
            GiftCards = giftCardRepository;
        }

        public IUserRepository Users { get; private set; }
        
        public IAddressRepository Addresses{ get; private set; }
        
        public IGiftCardRepository GiftCards { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
