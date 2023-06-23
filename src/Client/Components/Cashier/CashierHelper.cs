using FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient;

namespace FSH.BlazorWebAssembly.Client.Components.Cashier;

public class CashierHelper
{
}

public class BookingLineSummaryLocal
{
    public DateTime Date
    {
        get
        {
            DateTime date = Convert.ToDateTime(SourceList.Select(x => x.DateBooking).First());
            return date;
        }
    }

    public decimal? Amount
    {
        get
        {
            decimal value = SourceList.Select(x => x.Amount).First();
            return value;
        }
    }

    public decimal Price
    {
        get
        {
            decimal value = SourceList.Sum(x => x.Price);
            return value;
        }
    }

    public decimal Total
    {
        get
        {
            decimal value = Amount != null ? Price * Convert.ToDecimal(Amount) : Price;
            return value;
        }
    }

    public string Description
    {
        get
        {
            string value = SourceList.Select(x => x.Name).First();
            return value;
        }
    }

    public string ReferenceId
    {
        get
        {
            string value = SourceList.Count > 0 ? SourceList[0].BookingLineNumberId + SourceList[0].Source : string.Empty;
            return value;
        }
    }

    public int InvoicePosition
    {
        get
        {
            int value = SourceList.Select(x => x.InvoicePos).First();
            return (int)value;
        }
    }

    public string TaxLine
    {
        get
        {
            string value = string.Empty;
            var taxesGroupedByTaxRate = SourceList.GroupBy(x => x.TaxRate);
            foreach (var group in taxesGroupedByTaxRate)
            {
                value += "Tax: " + group.Key + "% T: " + (group.Sum(x => x.PriceTotal) / (100 + group.Key) * group.Key).ToString("N2") + " N: " + (group.Sum(x => x.PriceTotal) / (100 + group.Key) * 100).ToString("N2") + " Total: " + group.Sum(x => x.PriceTotal).ToString("N2") + " ";
            }

            return value;
        }
    }

    public List<BookingLine> SourceList { get; set; } = new();
}
