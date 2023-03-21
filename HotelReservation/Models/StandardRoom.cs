using HotelReservation.ViewModels.VmUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    public class StandardRoom : BaseNotifier, IRoom
    {
        private int _roomNumber { get; set; }
        private RoomTypeEnum _roomType { get; set; }
        private int _beds { get; set; }
        private int _size { get; set; }
        private bool _airConditioning { get; set; }
        private bool _television { get; set; }
        private bool _miniFridge { get; set; }

        public int RoomNumber { get => _roomNumber; set { _roomNumber = value; Notifica(nameof(RoomNumber)); } }
        public RoomTypeEnum RoomType { get => _roomType; set { _roomType = value; Notifica(nameof(RoomType)); } }
        public int Beds { get => _beds; set { _beds = value; Notifica(nameof(Beds)); } }
        public int Size { get => _size; set { _size = value; Notifica(nameof(Size)); } }
        public bool AirConditioning { get => _airConditioning; set { _airConditioning = value; Notifica(nameof(AirConditioning)); } }
        public bool Television { get => _television; set { _television = value; Notifica(nameof(Television)); } }
        public bool MiniFridge { get => _miniFridge; set { _miniFridge = value; Notifica(nameof(MiniFridge)); } }

        public StandardRoom() { }

        public StandardRoom(int roomNumber, RoomTypeEnum roomType, int beds, int size, bool airConditioning, bool television, bool miniFridge)
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
            Beds = beds;
            Size = size;
            AirConditioning = airConditioning;
            Television = television;
            MiniFridge = miniFridge;
        }

        public IRoom Clone()
        {
            return (StandardRoom) this.MemberwiseClone();
        }

        public void CopyRoom(IRoom newRoom)
        {
            StandardRoom newStandard = (StandardRoom)newRoom;
            RoomNumber = newStandard.RoomNumber;
            RoomType = newStandard.RoomType;
            Beds = newStandard.Beds;
            Size = newStandard.Size;
            AirConditioning = newStandard.AirConditioning;
            Television = newStandard.Television;
            MiniFridge = newStandard.MiniFridge;
        }
    }
}
