using FSH.BlazorWebAssembly.Client.ViewModels;

namespace FSH.BlazorWebAssembly.Client.Shared;

public interface IReportService<T> where T : class
{
    Task GenerateReport(string endpoint, List<T> reportData);
}
