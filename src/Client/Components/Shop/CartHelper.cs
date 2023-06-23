using FSH.BlazorWebAssembly.Client.Infrastructure.ApiClient;
using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices;

namespace FSH.BlazorWebAssembly.Client.Components.Shop;

public class CartHelper
{
    public int CartItemCount(List<CartItemMandantDto> CartItemList)
    {
        int result = 0;
        if (CartItemList != null)
        {
            if (CartItemList != null)
            {
                result = CartItemList.Count;
            }

        }

        return result;
    }

    public int CartItemCountAmount(List<CartItemMandantDto> CartItemList)
    {
        int result = 0;
        if (CartItemList != null)
        {
            foreach (var item in CartItemList)
            {
                result += Convert.ToInt16(item.Amount);
            }

        }

        return result;
    }

    public decimal CartPrice(List<CartItemMandantDto> CartItemList)
    {

        decimal result = 0;
        if (CartItemList != null)
        {
            foreach (var item in CartItemList)
            {
                result += ItemPriceTotal(item);
            }
        }

        return result;
    }

    public decimal ItemPriceTotal(CartItemMandantDto cartItem)
    {
        //return cartItem.Amount * cartItem.Price + Convert.ToDecimal(cartItem.PackageExtendedBookingLines.Sum(x => x.Total));
        return Convert.ToDecimal(cartItem.PackageExtendedBookingLines.Sum(x => x.Total));

    }
}

// @(cartMandant != null ? @Convert.ToDecimal(cartMandant.CartPrice).ToString("C2") : string.Empty)
// @(cartMandant != null ? cartMandant.CartItemCountAmount : 0)