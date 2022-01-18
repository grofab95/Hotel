using Hotel.Domain.Exceptions;
using System;

namespace Hotel.Domain.Entities;

public class DateRange
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }

    public DateRange(DateTime from, DateTime to)
    {
        if (from > to)
            throw new HotelException("Data do nie może być wcześniejsza od daty od.");

        From = from;
        To = to;
    }
}