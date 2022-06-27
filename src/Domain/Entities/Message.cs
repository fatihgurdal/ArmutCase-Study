namespace Domain.Entities
{
    public class Message : BaseEntity<Guid>
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
