namespace FSH.BlazorWebAssembly.Client.Pages.Shop;

//public class CartItemMandant
//{
//    public int Source { get; set; } // RoomReservation, Package, Wish, Message, MeetingReservation, Shop
//    public decimal Amount { get; set; } = 1;
//    public decimal Price { get; set; }
//    public decimal PriceTotal
//    {
//        get
//        {

//            return (Amount * Price) + Convert.ToDecimal(PackageExtendedBookingLines.Sum(x => x.Total));
//        }
//    }

//    public List<PriceCatDto>? PriceCats { get; set; }
//    public DateTime? Start { get; set; }
//    public DateTime? End { get; set; }
//    public string? Name { get; set; }
//    public int CategoryId { get; set; }
//    public Pax Pax { get; set; }
//    public int RateId { get; set; }
//    //public int CompanyId { get; set; }
//    //public int TravelAgentId { get; set; }
//    public List<PersonShopItem> PersonList { get; set; }
//    [StringLength(250)]
//    public string? Wishes { get; set; }
//    [StringLength(250)]
//    public string? Remarks { get; set; }
//    public string? ImagePath { get; set; }
//    public List<PackageExtendDto> PackageExtendedList { get; set; }
//    public List<BookingLineSummary> PackageExtendedBookingLines { get; set; }
//    public BookingPolicyDto BookingPolicy { get; set; }
//    public CancellationPolicyDto CancellationPolicy { get; set; }
//}

//public class PersonShopItem
//{
//    public int? PersonId { get; set; }
//    public string? Name { get; set; }
//    public string? FirstName { get; set; }
//    public string PersonShopType { get; set; }
//    public int ChildAge { get; set; }
//    public bool ExtraBed { get; set; }
//}

//public class CartMandant
//{
//public Guid CartId { get; set; }
//public int MandantId { get; set; }
//public int PersonId { get; set; }

//// geht nur bei Mandant im öffentlichen Shop kann nur
//// eine Person ausgewählt sein wenn ein Konto angelegt wurde.
//public bool BookerIsGuest { get; set; }

//public int CompanyId { get; set; }
//public int CompanyContactId { get; set; }

//public int TravelAgentId { get; set; }
//public int TravelAgentContactId { get; set; }

//// nur ShopMandant
//public int ShopPaymentId { get; set; }
//public DateTime HoldCartTill { get; set; }
//public string? MatchCode { get; set; }
//public string? Confirmations { get; set; }
//public List<CartItemMandant>? CartItemList { get; set; }

//public int CartItemCount
//{
//    get
//    {
//        int result = 0;
//        if (CartItemList != null)
//        {
//            result = CartItemList.Count;
//        }

//        return result;
//    }
//}

//public int CartItemCountAmount
//{
//    get
//    {
//        int result = 0;
//        if (CartItemList != null)
//        {
//            foreach (var item in CartItemList)
//            {
//                result += Convert.ToInt16(item.Amount);
//            }
//        }

//        return result;
//    }
//}

//public decimal? CartPrice
//{
//    get
//    {
//        decimal result = 0;
//        if (CartItemList != null)
//        {
//            foreach (var item in CartItemList)
//            {
//                result += item.PriceTotal;
//            }
//        }

//        return result;

//    }
//}

//    public BookingPolicyDto ValidBookingPolicy
//    {
//        get
//        {
//            BookingPolicyDto result = null;
//            if (CartItemList == null) return result;
//            if (CartItemList.Count == 1)
//            {
//                result = CartItemList[0].BookingPolicy;
//            }
//            else
//            {
//                int prio = 0;
//                foreach (CartItemMandant cartItemMandant in CartItemList)
//                {
//                    if (cartItemMandant.BookingPolicy.Priority > prio)
//                    {
//                        result = cartItemMandant.BookingPolicy;
//                    }
//                }
//            }

//            return result;
//        }
//    }

//    public CancellationPolicyDto ValidCancellationPolicy
//    {
//        get
//        {
//            CancellationPolicyDto result = null;
//            if (CartItemList == null) return result;
//            if (CartItemList.Count == 1)
//            {
//                result = CartItemList[0].CancellationPolicy;
//            }
//            else
//            {
//                int prio = 0;
//                foreach (CartItemMandant cartItemMandant in CartItemList)
//                {
//                    if (cartItemMandant.CancellationPolicy.Priority > prio)
//                    {
//                        result = cartItemMandant.CancellationPolicy;
//                    }
//                }
//            }

//            return result;
//        }
//    }

//}