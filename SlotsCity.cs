using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkinghouse_SQL;

internal class SlotsCity
{
    public int SlotNumber { get; set; }
    public bool ElectricOutlet { get; set; }
    public string HouseName { get; set; }
    public int ParkingHouseId { get; set; }
    public string CityName { get; set; }


    internal static void ShowSlotsCity(int id)
    {
        while (true)
        {
            Console.Clear();
            List<SlotsCity> slots = DatabasDapper.GetSlotsCity(id);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Plats\tEluttag\tHusnamn \tHusID\tStad");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (SlotsCity s in slots)
            {
                Console.WriteLine($"{s.SlotNumber}\t{s.ElectricOutlet}\t{s.HouseName.PadRight(8)}\t{s.ParkingHouseId}\t{s.CityName}");
            }
            
            Console.WriteLine("\nTryck I för att visa specifikt parkeringshus");
            Console.WriteLine("\nTryck T för att gå tillbaka");
            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case 'i':
                    Console.Write("\nAnge Id för parkeringshus: ");
                    ParkingSlots.Menu(int.Parse(Console.ReadLine()));
                    break;
                case 't':
                    return;
            }
        }
    }
}
