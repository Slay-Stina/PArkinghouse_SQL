using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Parkinghouse_SQL;

internal class DatabasADO
{
    public static List<Models.Car> GetAllCars()
    {
        string connString = "data source=.\\SQLEXPRESS; initial catalog=Parking; " +
            "persist security info=True; Integrated Security=true; TrustServerCertificate=True";
        string sql = "SELECT * from Cars";

        List<Models.Car> cars = new List<Models.Car>();

        using (var connection = new SqlConnection(connString))
        {
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Models.Car car = new Models.Car()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Plate = reader.GetString(reader.GetOrdinal("Plate")),
                            Make = reader.GetString(reader.GetOrdinal("Make")),
                            Color = reader.GetString(reader.GetOrdinal("Color")),
                            ParkingSlotsId = reader.IsDBNull(reader.GetOrdinal("ParkingSlotsId")) ? null : reader.GetInt32(reader.GetOrdinal("ParkingSlotsId"))
                        };
                        cars.Add(car);
                    }
                }
            }
            connection.Close();
        }
        return cars;
    }
}
