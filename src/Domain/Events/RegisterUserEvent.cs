namespace Domain.Events
{
    public class RegisterUserEvent : BaseEvent
    {
        public RegisterUserEvent(User user, string ipAddress)
        {
            User = user;
            IpAddress = ipAddress;
        }

        public User User { get; }
        public string IpAddress { get; }
    }
}
