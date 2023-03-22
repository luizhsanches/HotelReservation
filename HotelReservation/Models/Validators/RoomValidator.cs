using HotelReservation.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models.Validators
{
    public class RoomValidator
    {
        public void Validate(IRoom obj)
        {
            if (string.IsNullOrEmpty(obj.RoomNumber.ToString()) || obj.RoomNumber == 0)
            {
                throw new ArgumentException("Room number must not be empty or 0.");
            }

            if (string.IsNullOrEmpty(obj.Beds.ToString()) || obj.Beds == 0)
            {
                throw new ArgumentException("Beds must not be empty or 0.");
            }

            if (string.IsNullOrEmpty(obj.Size.ToString()) || obj.Size == 0)
            {
                throw new ArgumentException("Size must not be empty or 0.");
            }
        }
    }
}
