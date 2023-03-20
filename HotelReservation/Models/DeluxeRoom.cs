using HotelReservation.ViewModels.VmUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    public class DeluxeRoom : BaseNotifier, IDeluxeRoom
    {
        public int RoomNumber { get => RoomNumber; set { RoomNumber = value; Notifica(nameof(RoomNumber)); } }
        public int Beds { get => Beds; set { Beds = value; Notifica(nameof(Beds)); } }
        public int Size { get => Size; set { Size = value; Notifica(nameof(Size)); } }
        public bool AirConditioning { get => AirConditioning; set { AirConditioning = value; Notifica(nameof(AirConditioning)); } }
        public bool Television { get => Television; set { Television = value; Notifica(nameof(Television)); } }
        public bool MiniFridge { get => MiniFridge; set { MiniFridge = value; Notifica(nameof(MiniFridge)); } }
        public bool Jacuzzi { get => Jacuzzi; set { Jacuzzi = value; Notifica(nameof(Jacuzzi)); } }
        public bool Wifi { get => Wifi; set { Wifi = value; Notifica(nameof(Wifi)); } }

        public DeluxeRoom() { }

        public DeluxeRoom(int roomNumber, int beds, int size, bool airConditioning, bool television, bool miniFridge, bool jacuzzi, bool wifi)
        {
            RoomNumber = roomNumber;
            Beds = beds;
            Size = size;
            AirConditioning = airConditioning;
            Television = television;
            MiniFridge = miniFridge;
            Jacuzzi = jacuzzi;
            Wifi = wifi;
        }

        public IRoomType Clone()
        {
            return (DeluxeRoom)this.MemberwiseClone();
        }

        public void CopyRoom(IRoomType newRoom)
        {
            DeluxeRoom deluxeRoom = (DeluxeRoom)newRoom;
            RoomNumber = deluxeRoom.RoomNumber;
            Beds = deluxeRoom.Beds;
            Size = deluxeRoom.Size;
            AirConditioning = deluxeRoom.AirConditioning;
            Television = deluxeRoom.Television;
            MiniFridge = deluxeRoom.MiniFridge;
            Jacuzzi = deluxeRoom.Jacuzzi;
            Wifi = deluxeRoom.Wifi;
        }
    }
}
