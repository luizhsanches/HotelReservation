using HotelReservation.Models.Interfaces;
using HotelReservation.ViewModels.VmUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models.Rooms
{
    public class DeluxeRoom : BaseNotifier, IDeluxeRoom
    {
        private int _id { get; set; }
        private int _roomNumber { get; set; }
        private RoomTypeEnum _roomType { get; set; }
        private int _beds { get; set; }
        private int _size { get; set; }
        private bool _airConditioning { get; set; }
        private bool _television { get; set; }
        private bool _miniFridge { get; set; }
        private bool _jacuzzi { get; set; }
        private bool _wifi { get; set; }

        public int Id { get => _id; set { _id = value; Notifica(nameof(Id)); } }
        public int RoomNumber { get => _roomNumber; set { _roomNumber = value; Notifica(nameof(RoomNumber)); } }
        public RoomTypeEnum RoomType { get => _roomType; set { _roomType = value; Notifica(nameof(RoomType)); } }
        public int Beds { get => _beds; set { _beds = value; Notifica(nameof(Beds)); } }
        public int Size { get => _size; set { _size = value; Notifica(nameof(Size)); } }
        public bool AirConditioning { get => _airConditioning; set { _airConditioning = value; Notifica(nameof(AirConditioning)); } }
        public bool Television { get => _television; set { _television = value; Notifica(nameof(Television)); } }
        public bool MiniFridge { get => _miniFridge; set { _miniFridge = value; Notifica(nameof(MiniFridge)); } }
        public bool Jacuzzi { get => _jacuzzi; set { _jacuzzi = value; Notifica(nameof(Jacuzzi)); } }
        public bool Wifi { get => _wifi; set { _wifi = value; Notifica(nameof(Wifi)); } }

        public DeluxeRoom() { }

        public DeluxeRoom(int id, int roomNumber, RoomTypeEnum roomType, int beds, int size, bool airConditioning, bool television, bool miniFridge, bool jacuzzi, bool wifi)
        {
            Id = id;
            RoomNumber = roomNumber;
            RoomType = roomType;
            Beds = beds;
            Size = size;
            AirConditioning = airConditioning;
            Television = television;
            MiniFridge = miniFridge;
            Jacuzzi = jacuzzi;
            Wifi = wifi;
        }

        public IRoom Clone()
        {
            return (DeluxeRoom)this.MemberwiseClone();
        }

        public void CopyRoom(IRoom newRoom)
        {
            DeluxeRoom newDeluxe = (DeluxeRoom)newRoom;
            Id = newDeluxe.Id;
            RoomNumber = newDeluxe.RoomNumber;
            RoomType = newDeluxe.RoomType;
            Beds = newDeluxe.Beds;
            Size = newDeluxe.Size;
            AirConditioning = newDeluxe.AirConditioning;
            Television = newDeluxe.Television;
            MiniFridge = newDeluxe.MiniFridge;
            Jacuzzi = newDeluxe.Jacuzzi;
            Wifi = newDeluxe.Wifi;
        }
    }
}
