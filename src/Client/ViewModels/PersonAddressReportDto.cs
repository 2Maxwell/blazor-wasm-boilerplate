namespace FSH.BlazorWebAssembly.Client.ViewModels;

public class PersonAddressReportDto
{
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? Title { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Zip { get; set; }
    public string? City { get; set; }
    public int? CountryId { get; set; }
    public int? StateRegionId { get; set; }
    public string SalutationName { get; set; }
    public string SalutationLetterGreeting { get; set; }
    public string SalutationLetterClosing { get; set; }
}
