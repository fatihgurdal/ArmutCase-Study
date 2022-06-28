namespace Domain.Events
{
    public class UserLoginEvent : BaseEvent
    {
        public UserLoginEvent(User user, string ipAddress)
        {
            User = user;
            IpAddress = ipAddress;
        }

        public User User { get; }
        public string IpAddress { get; }
    }
}
