using FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient;
using System.Text.Json;

namespace FSH.BlazorWebAssembly.Client.Components.Reservation;

public class ReservationHelper
{

    public UpdateReservationRequest MapReservationDtoToReservationUpdateRequest(ReservationDto res)
    {
        UpdateReservationRequest upd = new UpdateReservationRequest();
        upd.Id = res.Id;
        upd.MandantId = res.MandantId;
        upd.ResKz = res.ResKz;
        upd.BookerId = res.BookerId;
        upd.GuestId = res.GuestId;
        upd.CompanyId = res.CompanyId;
        upd.CompanyContactId = res.CompanyContactId;
        upd.TravelAgentId = res.TravelAgentId;
        upd.TravelAgentContactId = res.TravelAgentContactId;
        upd.Persons = res.Persons;
        upd.Arrival = res.Arrival;
        upd.Departure = res.Departure;
        upd.CategoryId = res.CategoryId;
        upd.RoomAmount = res.RoomAmount;
        upd.RoomNumberId = res.RoomNumberId;
        upd.RoomNumber = res.RoomNumber;
        upd.RoomFixed = res.RoomFixed;
        upd.RateId = res.RateId;
        upd.RatePackages = res.RatePackages;
        upd.LogisTotal = res.LogisTotal;
        upd.BookingPolicyId = res.BookingPolicyId;
        upd.CancellationPolicyId = res.CancellationPolicyId;
        upd.IsGroupMaster = res.IsGroupMaster;
        upd.GroupMasterId = res.GroupMasterId;
        upd.Transfer = res.Transfer;
        upd.MatchCode = res.MatchCode;
        upd.OptionDate = res.OptionDate;
        upd.OptionFollowUp = res.OptionFollowUp;
        upd.CrsNumber = res.CrsNumber;
        upd.PaxString = res.PaxString;
        upd.CartId = res.CartId;
        upd.Confirmations = res.Confirmations;
        upd.Wishes = res.Wishes;
        upd.PriceTagDto = res.PriceTagDto;
        //upd.PriceTagDto.PriceTagDetails = res.PriceTagDto.PriceTagDetails;
        upd.PackageExtendOptionDtos = res.PackageExtendOptionDtos;
        return upd;
    }

    public string getPersonFullName(FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient.PersonDto person)
    {
        string fullName = string.Empty;

        if(person != null)
        {
        //TODO sollte eventl. PersonHelper sein.
        fullName += person.Title != null ? person.Title + " " : string.Empty;
        fullName += person.Name + " ";
        fullName += (person.FirstName + ", ") ?? string.Empty;
        //fullName += person.Salutation.Name;
        }

        return fullName;
    }

    public SearchMandantResQueryRequest ResQueryFromReservationDto(ReservationDto res)
    {
        SearchMandantResQueryRequest resQuery = new();
        resQuery.MandantId = res.MandantId;
        resQuery.Arrival = Convert.ToDateTime(res.Arrival).Date;
        resQuery.Departure = Convert.ToDateTime(res.Departure).Date;
        resQuery.SearchPersonId = res.BookerId;
        resQuery.SearchCompanyId = Convert.ToInt16(res.CompanyId);
        resQuery.RoomAmount = res.RoomAmount;
        resQuery.BedsTotal = 1;
        resQuery.PromotionCode = string.Empty;
        List<Pax> paxes = new();
        Pax? pax = JsonSerializer.Deserialize<Pax>(res!.PaxString!);
        paxes.Add(pax!);

        if (res.PaxString != null)
        {
            resQuery.RoomOccupancy = paxes;
        }

        return resQuery;
    }

    public ReservationDto SetReservationBackup(ReservationDto res)
    {
        ReservationDto reservation = new ReservationDto();
        reservation.Arrival = res.Arrival;
        reservation.Departure = res.Departure;
        reservation.PaxString = res.PaxString;
        reservation.CategoryId = res.CategoryId;
        return reservation;
    }

    public string CheckReservationDtoForPriceCalculation(ReservationDto reservation, ReservationDto backup)
    {
        string result = string.Empty;

        if (reservation.Arrival.Value.Date != backup.Arrival.Value.Date) result += "Arrival, "; // automatische Abfrage wenn notwendig
        if (reservation.Departure.Value.Date != backup.Departure.Value.Date) result += "Departure, "; // automatische Abfrage wenn notwendig

        Pax paxReservation = JsonSerializer.Deserialize<Pax?>(reservation.PaxString!);
        Pax paxBackup = JsonSerializer.Deserialize<Pax?>(backup.PaxString!);
        if (paxReservation.Adult + paxReservation.Children.Where(x => x.ExtraBed == true).Count() != paxBackup.Adult + paxBackup.Children.Where(x => x.ExtraBed == true).Count()) result += "Beds, "; // automatische Abfrage wenn möglich eventl. Categorywechsel empfehlen

        if (reservation.CategoryId != backup.CategoryId) result += "Category, "; // abfrage nach Upgrade oder neue Berechnung

        return result;
    }

    public PriceTagDto SetPriceTagChanged(PriceTagDto priceTag, List<PriceCatDto> priceCats)
    {
        foreach (PriceCatDto item in priceCats)
        {
            if ((priceTag.PriceTagDetails.Where(x => x.DatePrice.Date == item.DatePrice.Date).Count()) < 1)
            {
            PriceTagDetailDto ptDto = new PriceTagDetailDto();
            ptDto.RateStart = item.RateStart;
            ptDto.RateCurrent = item.RateCurrent;
            ptDto.EventDates = item.EventDates;
            ptDto.DatePrice = item.DatePrice;
            ptDto.CategoryId = item.CategoryId;
            ptDto.PaxAmount = item.Pax;
            ptDto.PriceTagId = priceTag.Id;
            ptDto.RateTypeEnumId = item.RateTypeEnumId;

            priceTag.PriceTagDetails.Add(ptDto);
            }
        }

        return priceTag;
    }

    public PriceTagDto SetNewPriceTag(PriceTagDto priceTag, List<PriceCatDto> priceCats)
    {
        // alte PriceTagDetail entkoppeln von PriceTag
        foreach (PriceTagDetailDto item in priceTag.PriceTagDetails)
        {
            item.PriceTagId = -1;
        }

        foreach (PriceCatDto item in priceCats)
        {
            PriceTagDetailDto ptDto = new PriceTagDetailDto();
            ptDto.RateStart = item.RateStart;
            ptDto.RateCurrent = item.RateCurrent;
            ptDto.EventDates = item.EventDates;
            ptDto.DatePrice = item.DatePrice;
            ptDto.CategoryId = item.CategoryId;
            ptDto.PaxAmount = item.Pax;
            ptDto.PriceTagId = priceTag.Id;
            ptDto.RateTypeEnumId = item.RateTypeEnumId;

            priceTag.PriceTagDetails.Add(ptDto);
        }

        // alte PriceTagDetail mit PriceTagId -1 löschen
        var count = priceTag.PriceTagDetails.ToList().RemoveAll(x => x.PriceTagId == -1);
        

        return priceTag;
    }

    public void CalculateAverageRate(PriceTagDto pricetag, DateTime Arrival, DateTime Departure)
    {
        // AverageRate berechnen nur mit PricetagDetail die auch gebucht sind. NoShow werden über den Nachtlauf mit
        // RateCurrent berechnet.
        decimal result = 0;

        decimal sum = pricetag.PriceTagDetails.Where(x => x.DatePrice >= Arrival.Date && x.DatePrice < Departure.Date && x.PriceTagId > 0).Sum(x => x.RateCurrent);
        int days = pricetag.PriceTagDetails.Where(x => x.DatePrice >= Arrival.Date && x.DatePrice < Departure.Date && x.PriceTagId > 0).Count();

        result = sum / days;

        foreach (PriceTagDetailDto item in pricetag.PriceTagDetails.Where(x => x.DatePrice >= Arrival.Date && x.DatePrice < Departure.Date && x.PriceTagId > 0))
        {
            item.AverageRate = result;
        }

        pricetag.Arrival = Arrival;
        pricetag.Departure = Departure;
        pricetag.AverageRate = result;
    }
}
