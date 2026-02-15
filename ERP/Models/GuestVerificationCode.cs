using System;

namespace ERP.Models
{
    public class GuestVerificationCode
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
        public int AttemptCount { get; set; }
    }
}
