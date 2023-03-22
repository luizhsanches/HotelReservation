using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models.Interfaces
{
    public interface IRoom
    {
        int Id { get; set; }
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
}
