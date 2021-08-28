namespace Infrastructure.Models.User
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
