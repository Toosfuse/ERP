namespace ERP.ViewModels.AssestViewModel
{
    public class AssetHistoryViewModel
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public int FromUserId { get; set; }
        public string FromUserName { get; set; }
        public int ToUserId { get; set; }
        public string ToUserName { get; set; }
        public string AssignDate { get; set; }
        public string Description { get; set; }
    }
}
