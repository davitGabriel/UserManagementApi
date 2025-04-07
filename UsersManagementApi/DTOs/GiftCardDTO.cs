namespace UsersManagementApi.DTOs
{
    public class GiftCardDTO
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsRedeemed { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
