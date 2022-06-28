namespace Application.Users.Queries.GetMessages
{
    public class MessageVm
    {
        //TODO: Mesajlar iki kişi arasında olduğu için json büyütmemek için Sender ve Receiver yerine tek boolean alan ekleyerek mesajın sahibi belirlenebilir.
        public Guid Sender { get; set; } //TODO: username olabilir. 
        public Guid Receiver { get; set; } //TODO: username olabilir
        /// <summary>
        /// UTC time
        /// </summary>
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
