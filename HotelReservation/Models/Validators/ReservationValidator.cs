using HotelReservation.Models.Reservations;
using System;

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

            if (obj.Length.TotalMinutes < 0)
            {
                throw new ArgumentException("Start Date must not be greater than End Date");
            }
        }
    }
}
