namespace CharityLink.Dtos.Donations
{
    public class MoMoNotify
    {
        public string PartnerCode { get; set; }
        public string OrderId { get; set; }
        public string RequestId { get; set; }
        public long Amount { get; set; }
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public string ExtraData { get; set; }
    }
}
