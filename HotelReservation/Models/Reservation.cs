using System;
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

        private IRoomType room;
        public IRoomType Room
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

        public Reservation(int id, IRoomType room, string username, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Room = room;
            Username = username;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
