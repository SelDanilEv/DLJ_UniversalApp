namespace Infrastructure.Models.User
{
    public class UserInfo : BaseModel
    {
        public string UserName { get; set; }
        public string PasswordFolderPath { get; set; }
    }
}
