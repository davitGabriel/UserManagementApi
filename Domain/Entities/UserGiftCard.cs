using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserGiftCard : BaseEntity
    {
        public int UserId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsRedeemed { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
