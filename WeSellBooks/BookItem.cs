using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSellBooks
{
    public class BookItem
    {
        public long? id;
        public string? asin;
        public string? title;
        public string? imageUrl;
        public decimal? price;
        public string? barcode;
        public bool? bonusItem;
        public decimal? discount;
        public long? catalogueProductId;
        public long? catalogueOptionId;
    }

    public class Message
    {
        public string? type;
        public string? message;
    }
    public class log
    {
    }
    public class basket
    {
        public decimal? offerTotal;
        public decimal? minimumOfferValue;
        public List<BookItem> items = new();
        public Message discountMessage;
        public decimal? blanketDiscount;
        public string? blanketDiscountMessage;
        public string? itemDiscountMessage;
    }
    internal class BasketItemResponse
    {
        public BookItem item;
        public basket basket;
        public List<log> logs = new();
        public decimal? price;
    }
}
