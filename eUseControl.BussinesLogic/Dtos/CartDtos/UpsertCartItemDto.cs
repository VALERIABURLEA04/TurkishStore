namespace businessLogic.Dtos.CartDtos
{
    public class UpsertCartItemDto
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
    }
}