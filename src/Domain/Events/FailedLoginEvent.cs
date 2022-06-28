namespace Domain.Events
{
    public class FailedLoginEvent : BaseEvent
    {
        public FailedLoginEvent(User user, string ipAddress)
        {
            User = user;
            IpAddress = ipAddress;
        }

        public User User { get; }
        public string IpAddress { get; }
    }
}
