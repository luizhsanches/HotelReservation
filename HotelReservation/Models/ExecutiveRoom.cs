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
        public int RoomNumber { get => RoomNumber; set { RoomNumber = value; Notifica(nameof(RoomNumber)); } }
        public int Beds { get => Beds; set { Beds = value; Notifica(nameof(Beds)); } }
        public int Size { get => Size; set { Size = value; Notifica(nameof(Size)); } }
        public bool AirConditioning { get => AirConditioning; set { AirConditioning = value; Notifica(nameof(AirConditioning)); } }
        public bool Television { get => Television; set { Television = value; Notifica(nameof(Television)); } }
        public bool MiniFridge { get => MiniFridge; set { MiniFridge = value; Notifica(nameof(MiniFridge)); } }
        public bool Jacuzzi { get => Jacuzzi; set { Jacuzzi = value; Notifica(nameof(Jacuzzi)); } }

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
