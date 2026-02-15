using System;

namespace ERP.Models
{
    public class GuestUser
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Image { get; set; } = "Male.png";
        public string UniqueToken { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastActivity { get; set; }
        public int? GroupId { get; set; }
    }
}
