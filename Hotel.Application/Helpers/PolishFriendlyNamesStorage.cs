using System;

namespace Hotel.Application.Helpers;

public class PolishFriendlyNamesStorage
{
    public static string GetFriendlyNameOfDay(int number)
    {
        if (number == 1)
            return "dzień";

        if (number == 0 || number >= 2)
            return "dni";

        throw new NotImplementedException();
    }

    public static string GetFriendlyNameOfWeek(int number)
    {
        if (number == 1)
            return "tydzień";

        if (number == 0 || number >= 5)
            return "tygodni";

        if (number >= 2 || number <= 4)
            return "tygodnie";

        throw new NotImplementedException();
    }

    public static string GetFriendlyNameOfMonth(int number)
    {
        if (number == 1)
            return "miesiąc";

        if (number == 0 || number >= 5)
            return "miesięcy";

        if (number >= 2 || number <= 4)
            return "miesiące";

        throw new NotImplementedException();
    }

    public static string GetFriendlyNameOfYear(int number)
    {
        if (number == 1)
            return "rok";

        if (number == 0 || number >= 5)
            return "lat";

        if (number >= 2 || number <= 4)
            return "lata";

        throw new NotImplementedException();
    }
}