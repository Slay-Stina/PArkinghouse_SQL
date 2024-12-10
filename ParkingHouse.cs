using Parkinghouse_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkinghouse_SQL
{
    internal class ParkingHouse
    {
        public int Id { get; set; }
        public string HouseName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }

        internal static void Menu()
        {
            while (true)
            {
                Console.Clear();
                List<ParkingHouse> houses = DatabasDapper.GetHouses();
                foreach (ParkingHouse p in houses)
                {
                    Console.WriteLine($"{p.Id}\t{p.HouseName.PadRight(8)}\t{p.CityName}");
                }
                Console.WriteLine();
                Console.WriteLine("Tryck L för att lägga till parkeringshus");
                Console.WriteLine("Tryck V för att visa parkeringsplatser.");
                Console.WriteLine("Tryck T för att gå tillbaka");

                var key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case 'l':
                        Console.Write("\nAnge namn på parkeringshuset: ");
                        DatabasDapper.insertHouse(Console.ReadLine());
                        break;
                    case 'v':
                        Console.Write("\nAnge Id: ");
                        ParkingSlots.ShowSlots(int.Parse(Console.ReadLine()));
                        Console.ReadKey();
                        break;
                    case 't':
                        return;
                }
            }
        }
    }
}
