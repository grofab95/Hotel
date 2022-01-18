using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;

namespace Hotel.Domain.Utilities;

public class CsvUtility
{
    public static List<string[]> ReadCsvFile(string filePath, string[] delimeters = null, bool hasFieldsEnclosedInQuotes = false)
    {
        var readedLines = new List<string[]>();

        using (TextFieldParser csvReader = new TextFieldParser(filePath))
        {
            csvReader.SetDelimiters(delimeters ?? new string[] { ";" }); ;
            csvReader.HasFieldsEnclosedInQuotes = hasFieldsEnclosedInQuotes;

            while (true)
            {
                if (csvReader.EndOfData)
                    break;

                readedLines.Add(csvReader.ReadFields());
            }
        }

        return readedLines;
    }
}