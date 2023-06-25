﻿using FSH.BlazorWebAssembly.Client.ViewModels;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace FSH.BlazorWebAssembly.Client.Shared;

public partial class ReportService<T> : IReportService<T> where T : class
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private const string BaseUrl = "https://localhost:5001/api/v1/Report/";

    public ReportService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
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
            var responseObject = JsonSerializer.Deserialize<ReportResponse>(responseContent);

            var reportBytes = Convert.FromBase64String(responseObject.result.fileContents);

            var reportFileName = responseObject.result.fileDownloadName;
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

