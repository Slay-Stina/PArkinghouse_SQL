using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkinghouse_SQL.Models;

internal class Cities
{
    public int Id { get; set; }
    public string CityName { get; set; }

    internal static void Menu()
    {
        while (true)
        {
            Console.Clear();
            ShowCities();
            Console.WriteLine();
            Console.WriteLine("Tryck L för att lägga till stad");
            Console.WriteLine("Tryck T för att gå tillbaka");

            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case 'l':
                    Console.Write("\nAnge stad: ");
                    DatabasDapper.insertCity(Console.ReadLine());
                    break;
                case 't':
                    return;
            }
        }
    }
    public static void ShowCities()
    {
        List<Cities> cities = DatabasDapper.GetCities();
        foreach (Cities c in cities)
        {
            Console.WriteLine($"{c.Id}\t{c.CityName}");
        }
    }
}
