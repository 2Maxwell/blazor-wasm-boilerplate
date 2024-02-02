using FSH.BlazorWebAssembly.Client.Components.Cashier;
using FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient;
using static FSH.BlazorWebAssembly.Client.Components.Invoice.CashierInvoiceAddress;

namespace FSH.BlazorWebAssembly.Client.Components.Persons;

public class PersonHelper
{
    public void PersonDtoToInvoiceAddress(PersonDto person, InvoiceAddress invoice)
    {
        invoice.Name1 = $"{person.SalutationName} {person.Title} {person.FirstName} {person.Name}";
        invoice.Address1 = person.Address1;
        invoice.Address2 = person.Address2;
        // TODO PersonDto hat kein Country
        // für eine korrekte Adresse benötigen wir aber ein Land
        // invoice.Country = person. Country;
        invoice.ZipCode = person.Zip;
        invoice.City = person.City;
        invoice.Email = person.Email;
    }

    public void PersonDtoToContactInvoiceAddress(PersonDto person, InvoiceAddress invoice)
    {
        invoice.ContactName = $"{person.SalutationName} {person.Title} {person.FirstName} {person.Name}";
        invoice.Email = person.Email is not null ? person.Email : invoice.Email;
    }

}
