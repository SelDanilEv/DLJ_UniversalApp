using System.Collections.Generic;

namespace Infrastructure.Models.PasswordManager
{
    public class AccountInfo
    {
        public string AccountName { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string Key { get; set; }
        
        public List<AccountInfoField> AdditionalFields { get; set; }
    }
}
