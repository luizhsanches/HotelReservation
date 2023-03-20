using HotelReservation.ViewModels.VmUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    public class ExecutiveRoom : BaseNotifier, IExecutiveRoom
    {
        private int _roomNumber { get; set; }
        private int _beds { get; set; }
        private int _size { get; set; }
        private bool _airConditioning { get; set; }
        private bool _television { get; set; }
        private bool _miniFridge { get; set; }
        private bool _jacuzzi { get; set; }

        public int RoomNumber { get => _roomNumber; set { _roomNumber = value; Notifica(nameof(RoomNumber)); } }
        public int Beds { get => _beds; set { _beds = value; Notifica(nameof(Beds)); } }
        public int Size { get => _size; set { _size = value; Notifica(nameof(Size)); } }
        public bool AirConditioning { get => _airConditioning; set { _airConditioning = value; Notifica(nameof(AirConditioning)); } }
        public bool Television { get => _television; set { _television = value; Notifica(nameof(Television)); } }
        public bool MiniFridge { get => _miniFridge; set { _miniFridge = value; Notifica(nameof(MiniFridge)); } }
        public bool Jacuzzi { get => _jacuzzi; set { _jacuzzi = value; Notifica(nameof(Jacuzzi)); } }

        public ExecutiveRoom() { }

        public ExecutiveRoom(int roomNumber, int beds, int size, bool airConditioning, bool television, bool miniFridge, bool jacuzzi)
        {
            RoomNumber = roomNumber;
            Beds = beds;
            Size = size;
            AirConditioning = airConditioning;
            Television = television;
            MiniFridge = miniFridge;
            Jacuzzi = jacuzzi;
        }

        public IRoomType Clone()
        {
            return (ExecutiveRoom)this.MemberwiseClone();
        }

        public void CopyRoom(IRoomType newRoom)
        {
            ExecutiveRoom newExecutive = (ExecutiveRoom)newRoom;
            RoomNumber = newExecutive.RoomNumber;
            Beds = newExecutive.Beds;
            Size = newExecutive.Size;
            AirConditioning = newExecutive.AirConditioning;
            Television = newExecutive.Television;
            MiniFridge = newExecutive.MiniFridge;
            Jacuzzi= newExecutive.Jacuzzi;
        }
    }
}
