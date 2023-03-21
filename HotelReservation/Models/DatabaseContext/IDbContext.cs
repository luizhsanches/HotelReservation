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
        int ExecuteQuery(NpgsqlCommand command, params NpgsqlParameter[] parameters);
        int InsertRoom(IRoom room);
        int UpdateRoom(IRoom room);
        int DeleteRoom(int roomId);
        ObservableCollection<Reservation> GetReservations();
        int InsertReservation(Reservation reservation);
        int UpdateReservation(Reservation reservation);
        int DeleteReservation(int reservationId);
    }
}
