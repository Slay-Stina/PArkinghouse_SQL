using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkinghouse_SQL.Models;

internal class Car
{
    public int Id { get; set; }
    public string Plate { get; set; }
    public string Make { get; set; }
    public string Color { get; set; }
    public int? ParkingSlotsId { get; set; }
    public string? HouseName { get; set; }
    internal static void Menu()
    {
        while (true)
        {
            Console.Clear();
            List<Car> cars = DatabasDapper.GetAllCars();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Id\tRegnr \tMärke\tFärg\tParkering");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Car car in cars)
            {
                Console.WriteLine(car.Id + "\t" + car.Plate + "\t" + car.Make + "\t" + car.Color + "\t" + car.HouseName);
            }
            Console.WriteLine();
            Console.WriteLine("Tryck L för att lägga till bil");
            Console.WriteLine("Tryck P för att parkera en bil");
            Console.WriteLine("Tryck C för att checka ut en bil");
            Console.WriteLine("Tryck T för att gå tillbaka");

            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case 'l':
                    Car car = new Car()
                    {
                        Plate = GetRandomPlate()
                    };
                    Console.Write("\nAnge märke: ");
                    car.Make = Console.ReadLine();
                    Console.Write("Ange färg: ");
                    car.Color = Console.ReadLine();
                    DatabasDapper.InsertCar(car);
                    break;
                case 'p':
                    Console.Write("\nAnge Id för bil du vill parkera: ");
                    int carId = int.Parse(Console.ReadLine());
                    ParkingHouse.ShowHouses();
                    Console.Write("Vilket parkeringshus vill du parkera i? Ange ID: ");
                    ParkingSlots.ShowSlots(int.Parse(Console.ReadLine()));
                    Console.Write("Vilken plats vill du stå på? Ange ID: ");
                    DatabasDapper.ParkCar(carId, int.Parse(Console.ReadLine()));
                    break;
                case 'c':
                    Console.Write("\nAnge Id för bil du vill checka ut: ");
                    int carId2 = int.Parse(Console.ReadLine());
                    DatabasDapper.ParkCar(carId2, null);
                    break;
                case 't':
                    return;
            }
        }
    }
    private static string GetRandomPlate()
    {
        string regplate = "";
        for (int i = 0; i < 3; i++)
        {
            regplate += (char)Random.Shared.Next('A', 'Z' + 1);
        }
        for (int i = 0; i < 3; i++)
        {
            regplate += Random.Shared.Next(10).ToString();
        }
        return regplate;
    }
}

