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

    public static void ShowSlots(int id)
    {
        //int ParkingHouseId = ParkingHouse.GetId();
        List<ParkingSlots> slots = DatabasDapper.GetSlots(id);
        foreach (ParkingSlots s in slots)
        {
            Console.WriteLine($"{s.Id}\t{s.SlotNumber}\t{s.ElectricOutlet}\t{s.ParkingHouseId}");
        }
    }
}
