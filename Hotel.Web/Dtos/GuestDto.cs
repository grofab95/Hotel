﻿namespace Hotel.Web.Dtos
{
    public class GuestDto
    {
        public string Name { get; set; }
        public bool IsChild { get; set; }
        public bool IsNewlyweds { get; set; }
        public bool OrderedBreakfest { get; set; }
        public decimal PriceForStay { get; set; }

        public GuestDto(string name, decimal priceForStay)
        {
            Name = name;
            PriceForStay = priceForStay;
        }
    }
}
