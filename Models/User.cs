namespace DemoJWT.Models
{
    public  class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public static List<User> users = new()
        {
            new User() { UserName = "shiv" , Password="shiv"}
        };
    }

    public class Jwt
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
