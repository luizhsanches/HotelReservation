using HotelReservation.Models.Rooms;
using HotelReservation.Models.Reservations;
using HotelReservation.Models.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models.DatabaseContext
{
    public interface IDbContext
    {
        ObservableCollection<IRoom> GetRooms();
        IRoom GetOneRoom(int roomId);
        bool GetRoomCountByRoomNumber(int roomNumber);        
        int InsertRoom(IRoom room);
        int UpdateRoom(IRoom room);
        int DeleteRoom(IRoom room);
        ObservableCollection<Reservation> GetReservations();
        bool GetReservationsByRoomId(int roomId);
        int InsertReservation(Reservation reservation);
        int UpdateReservation(Reservation reservation);
        int DeleteReservation(Reservation reservation);
        int ExecuteQuery(NpgsqlCommand command, params NpgsqlParameter[] parameters);
    }
}
