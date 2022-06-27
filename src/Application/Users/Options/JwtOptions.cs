namespace Application.Users.Options
{
    public class JwtOptions
    {
        public const string JwtOption = "JwtOption";
        public string Secret { get; set; } = string.Empty;
    }
}
