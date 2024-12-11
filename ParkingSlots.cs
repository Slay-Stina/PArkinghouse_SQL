using Parkinghouse_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkinghouse_SQL;

internal class ParkingSlots
{
    public int Id { get; set; }
    public int SlotNumber { get; set; }
    public bool ElectricOutlet { get; set; }
    public int ParkingHouseId { get; set; }
    public string HouseName { get; set; }
    public string Plate {  get; set; }

    public static void Menu(int id)
    {
        while (true)
        {
            Console.Clear();
            ShowSlots(id);
            

            Console.WriteLine("\nTryck L för att lägga till parkeringsplatser");
            Console.WriteLine("\nTryck T för att gå tillbaka");
            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case 'l':
                    Console.Write("\nAnge hur många parkeringsplatser som ska läggas till: ");
                    DatabasDapper.InsertSlots(id, int.Parse(Console.ReadLine()));
                    break;
                case 'i':
                    Console.Write("\nAnge Id för parkeringshus: ");
                    ParkingSlots.Menu(int.Parse(Console.ReadLine()));
                    break;
                case 't':
                    return;
            }
        }
    }

    public static void ShowSlots(int id)
    {
        List<ParkingSlots> slots = DatabasDapper.GetSlots(id);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ID\tPlats\tEluttag\tHusnamn \tHusID\tLedig");
        Console.ForegroundColor = ConsoleColor.White;

        foreach (ParkingSlots s in slots)
        {
            Console.WriteLine($"{s.Id}\t{s.SlotNumber}\t{s.ElectricOutlet}\t{s.HouseName.PadRight(8)}\t{s.ParkingHouseId}\t{(s.Plate == null ? "JA" : "NEJ")}");
        }
    }
}
