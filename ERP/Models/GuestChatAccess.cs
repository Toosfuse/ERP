using System;

namespace ERP.Models
{
    public class GuestChatAccess
    {
        public int Id { get; set; }
        public int GuestUserId { get; set; }
        public string AllowedUserId { get; set; } = null!;
        public DateTime GrantedDate { get; set; }
        
        public virtual GuestUser GuestUser { get; set; } = null!;
        public virtual Users AllowedUser { get; set; } = null!;
    }
}
