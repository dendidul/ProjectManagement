namespace Core.Dto.ExternalAPI.LoyaltyAPI
{
    public class LoyaltyResponseData<T>
    {
        public T? Data { get; set; }
        public LoyaltyStatus Status { get; set; }
        public DateTime TimeStamp { get; set; }

        public string TraceId { get; set; }

        public LoyaltyResponseData(LoyaltyStatus status, DateTime timeStamp)
        {
            Status = status;
            TimeStamp = timeStamp;
        }

        public LoyaltyResponseData() : this(new LoyaltyStatus(), DateTime.Now) { }

    }
}
