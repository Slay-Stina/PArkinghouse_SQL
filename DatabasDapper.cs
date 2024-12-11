using Dapper;
using Microsoft.Data.SqlClient;
using Parkinghouse_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkinghouse_SQL;

internal class DatabasDapper
{
    static string connString = "data source=.\\SQLEXPRESS; initial catalog=Parking; " +
        "persist security info=True; Integrated Security=true; TrustServerCertificate=True";

    //################# BILAR #########################
    public static List<Car> GetAllCars()
    {
        string sql = @"select Cars.Id, Plate, Make, Color, ParkingSlotsId, HouseName from Cars
                    left join ParkingSlots ps on
                    ps.Id = Cars.ParkingSlotsId
                    left join ParkingHouses ph on
                    ph.Id = ps.ParkingHouseId";
        List<Car> cars = new List<Car>();

        using (var connection = new SqlConnection(connString))
        {
            cars = connection.Query<Car>(sql).ToList();
        }
        return cars;
    }
    public static void InsertCar(Car car)
    {
        string sql = $"insert into Cars(Plate,Make,Color) values ('{car.Plate}', '{car.Make}','{car.Color}')";

        using (var connection = new SqlConnection(connString))
        {
            connection.Execute(sql);
        }
    }
    internal static void ParkCar(int carId, int? spotId)
    {
        string sql = $"update Cars set ParkingSlotsId = {(spotId == null ? "NULL" : spotId)} where Id = {carId}";

        using (var connection = new SqlConnection(connString))
        {
            connection.Execute(sql);
        }
    }

    //############### PARKERINGSPLATSER ##################
    public static List<AllSpots> GetAllFreeSpots()
    {
        string sql = 
            @"select ph.HouseName,
            COUNT(*) as FreeSlots
            from ParkingHouses ph
            join ParkingSlots ps on
            ps.ParkingHouseId = ph.Id
            left join Cars on
            Cars.ParkingSlotsId = ps.Id
            where cars.ParkingSlotsId is null
            group by ph.HouseName";

        List<AllSpots> allSpots = new List<AllSpots>();

        using(var connection = new SqlConnection(connString))
        {
            allSpots = connection.Query<AllSpots>(sql).ToList();
        }
        return allSpots;
    }
    //################### STÄDER ###########################
    public static List<Cities> GetCities()
    {
        var sql = "select * from Cities";
        List<Cities> allCities = new List<Cities>();

        using(var connection = new SqlConnection(connString))
        {
            allCities = connection.Query<Cities>(sql).ToList();
        }
        return allCities;
    }
    

    internal static void insertCity(string city)
    {
        string sql = $"insert into Cities(CityName) values ('{city}')";

        using (var connection = new SqlConnection(connString))
        {
            connection.Execute(sql);
        }
    }

    //################### PARKERINGSHUS ###########################
    public static List<ParkingHouse> GetHouses()
    {
        var sql = @"select ph.Id, HouseName, CityId,c.CityName, count(ps.ElectricOutlet) as NrOfOutlets
                    from ParkingHouses ph
                    join Cities c on 
                    c.Id = ph.CityId
                    join ParkingSlots ps on
                    ps.ParkingHouseId = ph.Id
                    group by ph.Id, HouseName, CityId,c.CityName";
        List<ParkingHouse> allHouses = new List<ParkingHouse>();

        using (var connection = new SqlConnection(connString))
        {
            allHouses = connection.Query<ParkingHouse>(sql).ToList();
        }
        return allHouses;
    }

    internal static void insertHouse(string? h)
    {
        Console.WriteLine("Vilken stad ligger parkeringshuset i? Ange Id: ");
        Cities.ShowCities();
        int cityId = int.Parse(Console.ReadLine());
        string sql = $"insert into ParkingHouses(HouseName,CityId) values ('{h}','{cityId}')";

        using (var connection = new SqlConnection(connString))
        {
            connection.Execute(sql);
        }
    }

    //###################### PARKERINGSPLATSER ############################
    internal static List<ParkingSlots> GetSlots(int id)
    {
        var sql = $"select ps.Id, SlotNumber,ElectricOutlet,ParkingHouseId,ParkingHouses.HouseName,Cars.Plate from ParkingSlots ps " +
            $"join ParkingHouses on ParkingHouses.Id = ps.ParkingHouseId " +
            $"left join Cars on ps.Id = Cars.ParkingSlotsId where ParkingHouseId = {id}";
        List<ParkingSlots> allSlots = new List<ParkingSlots>();

        using (var connection = new SqlConnection(connString))
        {
            allSlots = connection.Query<ParkingSlots>(sql).ToList();
        }
        return allSlots;
    }

    internal static List<SlotsCity> GetSlotsCity(int id)
    {
        var sql = $"select SlotNumber, ElectricOutlet, HouseName,ParkingHouseId, CityName from ParkingSlots join ParkingHouses on " +
            $"ParkingHouses.Id = ParkingSlots.ParkingHouseId join Cities on Cities.Id = ParkingHouses.CityId where CityId = {id}";
        List<SlotsCity> citySlots = new List<SlotsCity>();

        using (var connection = new SqlConnection(connString))
        {
            citySlots = connection.Query<SlotsCity>(sql).ToList();
        }
        return citySlots;
    }

    internal static void InsertSlots(int parkingHouseId, int slots)
    {
        for (int i = 1; i <= slots; i++)
        {
            string sql = $"insert into ParkingSlots(SlotNumber,ElectricOutlet,ParkingHouseId) values ({i},{Random.Shared.Next(2)},'{parkingHouseId}')";

            using (var connection = new SqlConnection(connString))
            {
                connection.Execute(sql);
            }
        }
    }
}
