namespace MichelMichels.DobissSharp.Models;

internal class FormattedStatusResponse
{
    public FormattedStatusResponse(int addressId)
    {
        this.AddressId = addressId;
    }

    public int AddressId { get; set; }
    public Dictionary<int, object> StatusByChannelId { get; set; } = [];
}
