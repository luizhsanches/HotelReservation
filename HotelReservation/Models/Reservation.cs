﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    public class Reservation
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private IRoom room;
        public IRoom Room
        {
            get { return room; }
            set { room = value; }
        }

        private string username;
        public string Username 
        { 
            get { return username; }
            set { username = value; }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public TimeSpan length => EndDate.Subtract(StartDate);

        public Reservation() { }

        public Reservation(int id, IRoom room, string username, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Room = room;
            Username = username;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Reservation Clone()
        {
            return (Reservation)MemberwiseClone();
        }

        public void CopyReservation(Reservation reservation)
        {
            Room = reservation.Room;
            Username = reservation.Username;
            StartDate = reservation.StartDate;
            EndDate = reservation.EndDate;
        }
    }
}
