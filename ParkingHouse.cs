using Parkinghouse_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkinghouse_SQL;

internal class ParkingHouse
{
    public int Id { get; set; }
    public string HouseName { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public int NrOfOutlets { get; set; }

    internal static void Menu()
    {
        while (true)
        {
            Console.Clear();
            ShowHouses();
            Console.WriteLine();
            Console.WriteLine("Tryck L för att lägga till parkeringshus");
            Console.WriteLine("Tryck I för att visa parkeringsplatser ID.");
            Console.WriteLine("Tryck S för att visa parkeringsplatser STAD.");
            Console.WriteLine("Tryck T för att gå tillbaka");

            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case 'l':
                    Console.Write("\nAnge namn på parkeringshuset: ");
                    DatabasDapper.insertHouse(Console.ReadLine());
                    break;
                case 'i':
                    Console.Write("\nAnge Id för parkeringshus: ");
                    ParkingSlots.Menu(int.Parse(Console.ReadLine()));
                    break;
                case 's':
                    Console.WriteLine();
                    Cities.ShowCities();
                    Console.Write("\nAnge Id för stad: ");
                    SlotsCity.ShowSlotsCity(int.Parse(Console.ReadLine()));
                    break;
                case 't':
                    return;
            }
        }
    }
    public static void ShowHouses()
    {
        List<ParkingHouse> houses = DatabasDapper.GetHouses();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("HusID\tHusnamn \tStad    \tAntal eluttag");
        Console.ForegroundColor = ConsoleColor.White;
        foreach (ParkingHouse p in houses)
        {
            Console.WriteLine($"{p.Id}\t{p.HouseName.PadRight(8)}\t{p.CityName.PadRight(8)}\t{p.NrOfOutlets}");
        }
    }
}
