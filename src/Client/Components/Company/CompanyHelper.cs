using FSH.BlazorWebAssembly.Client.Components.Cashier;
using FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient;
using static FSH.BlazorWebAssembly.Client.Components.Invoice.CashierInvoiceAddress;

namespace FSH.BlazorWebAssembly.Client.Components.Company;

public class CompanyHelper
{

    public void CompanyDtoToInvoiceAddress(CompanyDto company, InvoiceAddress invoice)
    {
        invoice.Name1 = company.Name1;
        invoice.Name2 = company.Name2;
        invoice.Address1 = company.Address1;
        invoice.Address2 = company.Address2;
        // invoice.Country = company.CountryName;
        invoice.ZipCode = company.Zip;
        invoice.City = company.City;
        invoice.Email = company.Email;
    }
}
