using FSH.BlazorWebAssembly.Client.ViewModels;
using static FSH.BlazorWebAssembly.Client.Pages.Identity.Roles.GenerateReportBookings;

namespace FSH.BlazorWebAssembly.Client.Shared;

public interface IReportService<T> where T : class
{
    Task GenerateReport(string endpoint, List<T> reportData);
    Task GenerateReportForBookings(BookingReportDto report);
}
