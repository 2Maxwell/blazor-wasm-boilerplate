using FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient;
using Microsoft.AspNetCore.Components;

namespace FSH.BlazorWebAssembly.Client.Components.Package;

public class PackageHelper
{
    public PackageHelper(IPackagesClient packagesClient)
    {
        this.packagesClient = packagesClient;
    }

    [Inject]
    protected IPackagesClient packagesClient { get; set; } = default!;
    public async Task<List<BookingLineSummary>> Calculate_PackageExtendeds(CartItemMandantDto cartItem, int mandantId)
    {
        List<BookingLineSummary> bookingLineSummaries = new();
        PackageExtendedCalculationRequest request = new();
        request.MandantId = mandantId;
        request.Arrival = Convert.ToDateTime(cartItem.Start);
        request.Departure = Convert.ToDateTime(cartItem.End);
        request.Pax = cartItem.Pax;
        request.RoomAmount = Convert.ToInt16(cartItem.Amount);
        request.PriceCatDtos = (ICollection<PriceCatDto>)cartItem.PriceCats;
        request.PackageExtendDtos = (ICollection<PackageExtendDto>)cartItem.PackageExtendedList;

        var result = await packagesClient.PackageExtendedCalculationRequestAsync(request);
        bookingLineSummaries = result.ToList();

        return bookingLineSummaries;
    }

}
