using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    public interface IRoomType
    {
        int RoomNumber { get; set; }
        int Beds { get; set; }
        int Size { get; set; }
        IRoomType Clone();
        void CopyRoom(IRoomType newRoom);
    }

    public interface IStandardRoom : IRoomType
    {
        bool AirConditioning { get; set; }
        bool Television { get; set; }
        bool MiniFridge { get; set; }
    }

    public interface IExecutiveRoom : IRoomType
    {
        bool AirConditioning { get; set; }
        bool Television { get; set; }
        bool MiniFridge { get; set; }
        bool Jacuzzi { get; set; }
    }

    public interface IDeluxeRoom : IRoomType
    {
        bool AirConditioning { get; set; }
        bool Television { get; set; }
        bool MiniFridge { get; set; }
        bool Jacuzzi { get; set; }
        bool Wifi { get; set; }
    }
}
