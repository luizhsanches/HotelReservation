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
        RoomTypeEnum RoomType { get; set; }
        int Beds { get; set; }
        int Size { get; set; }
        bool AirConditioning { get; set; }
        bool Television { get; set; }
        bool MiniFridge { get; set; }
        IRoom Clone();
        void CopyRoom(IRoom newRoom);
    }

    public interface IExecutiveRoom : IRoom
    {
        bool Jacuzzi { get; set; }
    }

    public interface IDeluxeRoom : IExecutiveRoom
    {
        bool Wifi { get; set; }
    }
}
