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

    public int ReservationId
    {
        get
        {
            int reservationId = SourceList.Select(x => x.ReservationId).First();
            return reservationId;
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

    public decimal BruttoByTaxRate(decimal taxRate)
    {
        decimal value = SourceList.Where(x => x.TaxRate == taxRate).Sum(x => x.PriceTotal);
        return value;
    }

    public decimal NettoByTaxRate(decimal taxRate)
    {
        decimal value = SourceList.Where(x => x.TaxRate == taxRate).Sum(x => x.PriceTotal) / (100 + taxRate) * 100;
        return value;
    }

    public decimal TaxByTaxRate(decimal taxRate)
    {
        decimal value = SourceList.Where(x => x.TaxRate == taxRate).Sum(x => x.PriceTotal) / (100 + taxRate) * taxRate;
        return value;
    }

    public bool Debit
    {
        get
        {
            bool value = SourceList.Select(x => x.Debit).First();
            return value;
        }
    }

    public List<BookingLine> SourceList { get; set; } = new();
}

public class InvoiceAddress
{
    public string? Name1 { get; set; }
    public string? Name2 { get; set; }
    public string? ContactName { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Email { get; set; }
    public bool SendEmail { get; set; }
}

public class InvoiceTax
{
    public decimal TaxRate { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal NetAmount { get; set; }
    public decimal TotalAmount { get; set; }
}

public class InvoicePayment
{
    public DateTime HotelDate { get; set; }
    public string? Name { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public bool Debit { get; set; }
    public int ItemId { get; set; }
    public int ItemNumber { get; set; }
    public int TaxId { get; set; }
    public decimal TaxRate { get; set; }
    public int? KasseId { get; set; }
}

public enum CashierFunctionEnum
{
    None,
    TransferToRoom,
    ChangeInvoicePosition,
    SplitAmount,
    SplitPrice,
    MergeSummaries,
}

public class CashierBalance
{
    public CashierBalance(string description, string split, decimal debit, decimal credit)
    {
        Description = description;
        Split = split;
        Debit = debit;
        Credit = credit;
    }

    public string Description { get; set; }
    public string Split { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
}
