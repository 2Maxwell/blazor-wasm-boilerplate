using FSH.BlazorWebAssembly.Client.ViewModels;
using Microsoft.JSInterop;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static FSH.BlazorWebAssembly.Client.Pages.Identity.Roles.GenerateReportBookings;

namespace FSH.BlazorWebAssembly.Client.Shared;

public partial class ReportService<T> : IReportService<T>
     where T : class
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private const string BaseUrl = "https://localhost:5001/api/v1/Report/";

    public ReportService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task ProcessReportWriter(string responseType, string reportEndpoint, string jsonRequest)
    {
        string endPoint = "https://localhost:5001/api/v1/Report/";
        // string reportEndpoint = "rptreservations";
        string endPointToSend = endPoint + reportEndpoint;
        // var jsonData = JsonSerializer.Serialize(request);
        var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endPointToSend, httpContent);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<ReportResult>(responseContent);

            var reportBytes = Convert.FromBase64String(responseObject.fileContents);
            var reportFileName = responseObject.fileDownloadName;
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string[] reportFileNameParts = reportFileName.Split('.');
            var fileName = $"{reportFileNameParts[0]}_{timestamp}.{reportFileNameParts[1]}";

            var memoryStream = new MemoryStream(reportBytes);
            var downloadStream = new MemoryStream();
            await memoryStream.CopyToAsync(downloadStream);
            downloadStream.Position = 0;

            // var mimeType = "application/pdf";
            await _jsRuntime.InvokeVoidAsync("saveAsFile", fileName, downloadStream.ToArray());
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        }

    }

    public async Task GenerateReport(string endpoint, List<T> reportData)
    {
        string endpointToSend = BaseUrl + endpoint;
        var jsonData = JsonSerializer.Serialize(reportData);
        var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpointToSend, httpContent);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<ReportResult>(responseContent);

            var reportBytes = Convert.FromBase64String(responseObject.fileContents);

            var reportFileName = responseObject.fileDownloadName;
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileName = $"{timestamp}_{reportFileName}";

            var memoryStream = new MemoryStream(reportBytes);
            var downloadStream = new MemoryStream();
            await memoryStream.CopyToAsync(downloadStream);
            downloadStream.Position = 0;

            var mimeType = "application/pdf";

            await _jsRuntime.InvokeVoidAsync("saveAsFile", fileName, downloadStream.ToArray());
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        }
    }

    public async Task GenerateReportForBookings(BookingReportDto report)
    {
        string endpointToSend = "https://localhost:5001/api/v1/Booking/bookingsReport";
        var jsonData = JsonSerializer.Serialize(report);
        var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpointToSend, httpContent);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<ReportResult>(responseContent);

            var reportBytes = Convert.FromBase64String(responseObject.fileContents);

            var reportFileName = responseObject.fileDownloadName;
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileName = $"{timestamp}_{reportFileName}";

            var memoryStream = new MemoryStream(reportBytes);
            var downloadStream = new MemoryStream();
            await memoryStream.CopyToAsync(downloadStream);
            downloadStream.Position = 0;

            var mimeType = "application/pdf";

            await _jsRuntime.InvokeVoidAsync("saveAsFile", fileName, downloadStream.ToArray());
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        }
    }
}

