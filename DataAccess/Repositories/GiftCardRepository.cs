using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class GiftCardRepository : GenericRepository<UserGiftCard>, IGiftCardRepository
    {
        public GiftCardRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<UserGiftCard> GetGiftCardsByUserId(int userId)
        {
            return _context.Set<UserGiftCard>().Where(g => g.UserId == userId);
        }
    }
}