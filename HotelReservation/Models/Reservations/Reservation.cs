﻿using HotelReservation.Models.Interfaces;
using HotelReservation.ViewModels.VmUtils;
using System;

namespace HotelReservation.Models.Reservations
{
    public class Reservation : BaseNotifier
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int roomId;
        public int RoomId
        {
            get { return roomId; }
            set { roomId = value; }
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
            set { username = value; Notifica(nameof(Username)); }
        }

        private DateTime startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; Notifica(nameof(StartDate)); }
        }

        private DateTime endDate = DateTime.Now.AddDays(1);
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; Notifica(nameof(EndDate)); }
        }

        public TimeSpan Length => EndDate.Subtract(StartDate);

        public Reservation() { }

        public Reservation(int id, int roomId, IRoom room, string username, DateTime startDate, DateTime endDate)
        {
            Id = id;
            RoomId = roomId;
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
