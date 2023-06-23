using FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient;

namespace FSH.BlazorWebAssembly.Client.Pages.Shop;

public class PaxTransferService
{
    public int RoomNumber { get; set; }
    public int Adults { get; set; }
    public List<Child>? ChildsList { get; set; }
    public override string ToString()
    {
        string strChilds = string.Empty;
        if (ChildsList != null)
        {
            foreach (Child child in ChildsList)
            {
                strChilds += child.Age + "|";
                strChilds += child.ExtraBed + "|";
            }
        }

        return RoomNumber + "|" + Adults + "|" + strChilds;
    }

}

public class ResQueryTransferService
{
    public string? DestinationCountry { get; set; }
    public string? DestinationTown { get; set; }
    public string? DestinationZipCode { get; set; }
    public string? DestinationDecimalCoordinates { get; set; }
    public int SearchPersonId { get; set; }
    public int SearchCompanyId { get; set; }
    public DateTime? Arrival { get; set; }
    public DateTime? Departure { get; set; }
    public string? PromotionCode { get; set; }
    public List<PaxTransferService> PaxTransferServiceList { get; set; } = new();
    public int RoomAmount { get; set; }
    public ResQueryTransferService()
    {

    }

    public ResQueryTransferService(ResQuery resQuery)
    {
        DestinationCountry = resQuery.DestinationCountry;
        DestinationTown = resQuery.DestinationTown;
        DestinationZipCode = resQuery.DestinationZipCode;
        DestinationDecimalCoordinates = resQuery.DestinationDecimalCoordinates;
        SearchPersonId = resQuery.SearchPersonId;
        SearchCompanyId = resQuery.SearchCompanyId;
        PromotionCode = resQuery.PromotionCode;
        Arrival = resQuery.Arrival;
        Departure = resQuery.Departure;
        foreach (Pax pax in resQuery.RoomOccupancy)
        {
            int roomNumber = 0;
            PaxTransferService pts = new();
            pts.RoomNumber = roomNumber;
            pts.Adults = pax.Adult;
            if (pax.Children != null)
            {
                pts.ChildsList = new();
                pts.ChildsList = (List<Child>)pax.Children;
            }

            roomNumber++;
            PaxTransferServiceList.Add(pts);
        }

        RoomAmount = PaxTransferServiceList.Count();
    }

    public override string ToString()
    {
        string result = string.Empty;
        result += DestinationCountry + "&";
        result += DestinationTown + "&";
        result += DestinationZipCode + "&";
        result += DestinationDecimalCoordinates + "&";
        result += SearchPersonId + "&";
        result += SearchCompanyId + "&";
        result += PromotionCode + "&";
        result += Arrival + "&";
        result += Departure + "&";
        foreach (PaxTransferService item in PaxTransferServiceList)
        {
            result += item.ToString() + ";";
        }

        return result;
    }
}