using HotelReservation.ViewModels.VmUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    public class StandardRoom : BaseNotifier, IStandardRoom
    {        
        public int RoomNumber { get => RoomNumber; set { RoomNumber = value; Notifica(nameof(RoomNumber)); } }
        public int Beds { get => Beds; set { Beds = value; Notifica(nameof(Beds)); } }
        public int Size { get => Size; set { Size = value; Notifica(nameof(Size)); } }
        public bool AirConditioning { get => AirConditioning; set { AirConditioning = value; Notifica(nameof(AirConditioning)); } }
        public bool Television { get => Television; set { Television = value; Notifica(nameof(Television)); } }
        public bool MiniFridge { get => MiniFridge; set { MiniFridge = value; Notifica(nameof(MiniFridge)); } }

        public StandardRoom() { }

        public StandardRoom(int roomNumber, int beds, int size, bool airConditioning, bool television, bool miniFridge)
        {
            RoomNumber = roomNumber;
            Beds = beds;
            Size = size;
            AirConditioning = airConditioning;
            Television = television;
            MiniFridge = miniFridge;
        }

        public IRoomType Clone()
        {
            return (StandardRoom) this.MemberwiseClone();
        }

        public void CopyRoom(IRoomType newRoom)
        {
            StandardRoom newStandard = (StandardRoom)newRoom;
            RoomNumber = newStandard.RoomNumber;
            Beds = newStandard.Beds;
            Size = newStandard.Size;
            AirConditioning = newStandard.AirConditioning;
            Television = newStandard.Television;
            MiniFridge = newStandard.MiniFridge;
        }
    }
}
