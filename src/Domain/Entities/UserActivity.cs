using Domain.Enums;

namespace Domain.Entities
{
    public class UserActivity
    {
        public DateTime Date { get; set; }
        public string IpAddress { get; set; }
        public string ActivityDescription { get; set; }
        public UserActivityType Type { get; set; }
    }
}
