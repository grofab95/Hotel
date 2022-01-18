using Hotel.Domain.Entities;
using Hotel.Domain.Extensions;
using MariGold.OpenXHTML;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hotel.Application.Services;

public class ReservationDocumentService
{
    private Reservation _reservation;
    private StringBuilder _html;

    private string _document;

    public ReservationDocumentService(Reservation reservation)
    {
        _reservation = reservation;
        _html = new StringBuilder();
        _document = $"rezerwacja-{_reservation.Id}";

        if (!Directory.Exists(".\\wwwroot\\documents"))
            Directory.CreateDirectory(".\\wwwroot\\documents");
    }

    public string GetCreatedDocumentName()
    {
        PrepareFile();
        BuildTable();
        Save();

        return _document;
    }

    private void PrepareFile()
    {
        var initialHtml = File.ReadAllLines("data/head.txt").ToList();

        var refilled = RefillInfos(initialHtml);

        refilled.ForEach(x => _html.AppendLine(x));
    }

    private List<string> RefillInfos(List<string> initialHtml)
    {
        var refilledLines = new List<string>();

        foreach (var line in initialHtml)
        {
            var refilledLine = line;

            refilledLine = refilledLine.Replace("[#ID#]", _reservation.Id.ToString());
            refilledLine = refilledLine.Replace("[#DATE_RANGE#]", $"{_reservation.CheckIn:dd.MM.yyyy}-{_reservation.CheckOut:dd.MM.yyy}");
            refilledLine = refilledLine.Replace("[#ROOMS_AMOUNT#]", _reservation.GetRoomsAmount().ToString());
            refilledLine = refilledLine.Replace("[#GUESTS_ORDER#]", $"{_reservation.GetGuestsAmount()}/{_reservation.GetTotalRoomsCapacity()}");
            refilledLine = refilledLine.Replace("[#TOTAL#]", $"{_reservation.TotalPrice} zł");

            refilledLines.Add(refilledLine);
        }

        return refilledLines;
    }

    private void BuildTable()
    {
        foreach (var reservationRoom in _reservation.ReservationRooms)
        {
            foreach (var guest in reservationRoom.Guests)
            {
                _html.AppendLine("<tr>");
                if (reservationRoom.Guests.IndexOf(guest) == 0)
                    _html.AppendLine($"<td style=\"border: 1px solid black; \" rowspan=\"{reservationRoom.Guests.Count()}\">  {reservationRoom.Room}  </td>");

                _html.AppendLine($"<td style=\"border: 1px solid black; \">  {guest.Name}  </td>");
                _html.AppendLine($"<td style=\"border: 1px solid black; \">  {guest.IsNewlyweds.GetName().Replace("Nie", "")}  </td>");
                _html.AppendLine($"<td style=\"border: 1px solid black; \">  {guest.IsChild.GetName().Replace("Nie", "")}  </td>");
                _html.AppendLine($"<td style=\"border: 1px solid black; \">  {guest.OrderedBreakfest.GetName().Replace("Nie", "")}  </td>");

                _html.AppendLine("</tr>");
            }
        }
    }

    private void Save()
    {
        _html.AppendLine("</tbody></table>");

        WordDocument doc = new WordDocument($".\\wwwroot\\documents\\{_document}.docx");

        doc.Process(new MariGold.OpenXHTML.HtmlParser(_html.ToString()));
        doc.Save();
    }
}