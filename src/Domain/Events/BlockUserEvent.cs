namespace Domain.Events
{
    public class BlockUserEvent : BaseEvent
    {
        public BlockUserEvent(User user, Guid blockedUserId)
        {
            User = user;
            BlockedUserId = blockedUserId;
        }

        public User User { get; }
        public Guid BlockedUserId { get; }
    }
}
