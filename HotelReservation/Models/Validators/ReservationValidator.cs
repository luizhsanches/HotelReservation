using HotelReservation.Models.Classes;
using HotelReservation.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservation.Models.Validators
{
    public class ReservationValidator
    {
        public void Validate(Reservation obj)
        {
            if (string.IsNullOrEmpty(obj.Username) || obj.Username.Length < 3)
            {
                throw new ArgumentException("Username must be at least 3 characters long.");
            }

            if (obj.length.TotalMinutes < 0)
            {
                throw new ArgumentException("Start Date must not be greater than End Date");
            }
        }
    }
}
