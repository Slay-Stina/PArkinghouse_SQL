using Parkinghouse_SQL.Models;

namespace Parkinghouse_SQL;

internal class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Databas via Dapper");

        

        List<Models.AllSpots> allSpots = DatabasDapper.GetAllSpots();

        foreach(Models.AllSpots spot in allSpots)
        {
            Console.WriteLine($"{spot.HouseName.PadRight(8)}\t{spot.PlatserPerHus}\t{spot.Slots}");
        }
        
            Console.WriteLine();
            Console.WriteLine("Tryck S för städer");
            Console.WriteLine("Tryck B för bilar");
            Console.WriteLine("Tryck H för parkeringshus");

            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case 's':
                    Cities.Menu();
                    break;
                case 'b':
                    Car.Menu();
                    break;
                case 'h':
                    ParkingHouse.Menu();
                    break;
            }
            Console.Clear();
        }
    }
}
