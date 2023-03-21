using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    public interface IRoom
    {
        int RoomNumber { get; set; }
        int Beds { get; set; }
        int Size { get; set; }
        IRoom Clone();
        void CopyRoom(IRoom newRoom);
    }

    public interface IStandardRoom : IRoom
    {
        bool AirConditioning { get; set; }
        bool Television { get; set; }
        bool MiniFridge { get; set; }
    }

    public interface IExecutiveRoom : IRoom
    {
        bool AirConditioning { get; set; }
        bool Television { get; set; }
        bool MiniFridge { get; set; }
        bool Jacuzzi { get; set; }
    }

    public interface IDeluxeRoom : IRoom
    {
        bool AirConditioning { get; set; }
        bool Television { get; set; }
        bool MiniFridge { get; set; }
        bool Jacuzzi { get; set; }
        bool Wifi { get; set; }
    }
}
