namespace MichelMichels.DobissSharp.Models
{
    internal class FormattedStatusResponse
    {
        public FormattedStatusResponse(int addressId)
        {
            this.AddressId = addressId;
            StatusByChannelId = new Dictionary<int, object>();
        }

        public int AddressId { get; set; }
        public Dictionary<int, object> StatusByChannelId { get; set; }
    }
}
