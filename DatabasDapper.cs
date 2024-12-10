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
        string sql = "select * from Cars";
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
    public static List<AllSpots> GetAllSpots()
    {
        string sql = @"select 
                        ph.HouseName,
                        COUNT(*) as PlatserPerHus,
                        STRING_AGG(ps.SlotNumber, ', ') as Slots
                        from ParkingHouses ph
                        join ParkingSlots ps on
                        ps.ParkingHouseId = ph.Id
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
        var sql = "select p.Id, HouseName, CityId,c.CityName from ParkingHouses p join Cities c on c.Id = p.CityId";
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
        var sql = $"select * from ParkingSlots where ParkingHouseId = {id}";
        List<ParkingSlots> allSlots = new List<ParkingSlots>();

        using (var connection = new SqlConnection(connString))
        {
            allSlots = connection.Query<ParkingSlots>(sql).ToList();
        }
        return allSlots;
    }
}
