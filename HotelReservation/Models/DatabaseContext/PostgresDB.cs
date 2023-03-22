using HotelReservation.Models.Rooms;
using HotelReservation.Models.Interfaces;
using HotelReservation.Models.Reservations;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservation.Models.DatabaseContext
{
    public class PostgresDB : IDbContext
    {
        private readonly string connectionString;

        public PostgresDB(bool isTestDb = false)
        {
            if (isTestDb)
            {
                connectionString = "Server=localhost;Port=5432;Database=hotel_reservation_test;User Id=postgres;Password=1234;Integrated Security=true;";
            }
            else
            {
                connectionString = "Server=localhost;Port=5432;Database=hotel_reservation;User Id=postgres;Password=1234;Integrated Security=true;";
            }
        }

        public ObservableCollection<IRoom> GetRooms()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                IRoom room;
                string selectQuery = "SELECT * FROM rooms";
                using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        ObservableCollection<IRoom> rooms = new ObservableCollection<IRoom>();
                        NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _ = Enum.TryParse(reader.GetString(2), out RoomTypeEnum roomType);
                            if (roomType == RoomTypeEnum.Standard)
                            {
                                room = new StandardRoom
                                {
                                    Id = (int)reader["id"],
                                    RoomNumber = (int)reader["room_number"],
                                    RoomType = roomType,
                                    Beds = (int)reader["beds"],
                                    Size = (int)reader["size"],
                                    AirConditioning = (bool)reader["air_conditioning"],
                                    Television = (bool)reader["television"],
                                    MiniFridge = (bool)reader["mini_fridge"]
                                };
                            }
                            else if (roomType == RoomTypeEnum.Executive)
                            {
                                room = new ExecutiveRoom
                                {
                                    Id = (int)reader["id"],
                                    RoomNumber = (int)reader["room_number"],
                                    RoomType = roomType,
                                    Beds = (int)reader["beds"],
                                    Size = (int)reader["size"],
                                    AirConditioning = (bool)reader["air_conditioning"],
                                    Television = (bool)reader["television"],
                                    MiniFridge = (bool)reader["mini_fridge"],
                                    Jacuzzi = (bool)reader["jacuzzi"]
                                };
                            }
                            else
                            {
                                room = new DeluxeRoom
                                {
                                    Id = (int)reader["id"],
                                    RoomNumber = (int)reader["room_number"],
                                    RoomType = roomType,
                                    Beds = (int)reader["beds"],
                                    Size = (int)reader["size"],
                                    AirConditioning = (bool)reader["air_conditioning"],
                                    Television = (bool)reader["television"],
                                    MiniFridge = (bool)reader["mini_fridge"],
                                    Jacuzzi = (bool)reader["jacuzzi"],
                                    Wifi = (bool)reader["wifi"]
                                };
                            }

                            rooms.Add(room);
                        }
                        reader.Close();
                        return rooms;
                    }
                    catch (NpgsqlException npg)
                    {
                        throw new Exception("An error ocurred when accessing database: " + npg.Message);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error when loading rooms: " + ex.Message);
                    }
                }
            }
        }

        public IRoom GetOneRoom(int roomId)
        {            
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                IRoom room;
                string selectQuery = "SELECT * FROM rooms WHERE id = @id";
                
                using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, conn))
                {
                    command.Parameters.AddWithValue("@id", roomId);
                    try
                    {
                        conn.Open();
                        command.Connection = conn;
                        
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            _ = Enum.TryParse(reader.GetString(2), out RoomTypeEnum roomType);
                            if (roomType == RoomTypeEnum.Standard)
                            {
                                room = new StandardRoom
                                {
                                    Id = (int)reader["id"],
                                    RoomNumber = (int)reader["room_number"],
                                    RoomType = roomType,
                                    Beds = (int)reader["beds"],
                                    Size = (int)reader["size"],
                                    AirConditioning = (bool)reader["air_conditioning"],
                                    Television = (bool)reader["television"],
                                    MiniFridge = (bool)reader["mini_fridge"]
                                };
                                return room;
                            }
                            else if (roomType == RoomTypeEnum.Executive)
                            {
                                room = new ExecutiveRoom
                                {
                                    Id = (int)reader["id"],
                                    RoomNumber = (int)reader["room_number"],
                                    RoomType = roomType,
                                    Beds = (int)reader["beds"],
                                    Size = (int)reader["size"],
                                    AirConditioning = (bool)reader["air_conditioning"],
                                    Television = (bool)reader["television"],
                                    MiniFridge = (bool)reader["mini_fridge"],
                                    Jacuzzi = (bool)reader["jacuzzi"]
                                };
                                return room;
                            }
                            else if (roomType == RoomTypeEnum.Deluxe)
                            {
                                room = new DeluxeRoom
                                {
                                    Id = (int)reader["id"],
                                    RoomNumber = (int)reader["room_number"],
                                    RoomType = roomType,
                                    Beds = (int)reader["beds"],
                                    Size = (int)reader["size"],
                                    AirConditioning = (bool)reader["air_conditioning"],
                                    Television = (bool)reader["television"],
                                    MiniFridge = (bool)reader["mini_fridge"],
                                    Jacuzzi = (bool)reader["jacuzzi"],
                                    Wifi = (bool)reader["wifi"]
                                };
                                return room;
                            }
                            else
                            {
                                return null;
                            }
                            
                        }
                        reader.Close();
                        return null;
                    }
                    catch (NpgsqlException npg)
                    {
                        throw new Exception("An error ocurred when accessing database: " + npg.Message);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error when loading room: " + ex.Message);
                    }
                }
            }
        }

        public bool GetRoomCountByRoomNumber(int roomNumber)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                string countQuery = "Select COUNT(*) from rooms WHERE room_number = @room_number";

                using (NpgsqlCommand command = new NpgsqlCommand(countQuery, conn))
                {
                    command.Parameters.AddWithValue("@room_number", roomNumber);

                    try
                    {
                        conn.Open();
                        command.Connection = conn;

                        return Convert.ToBoolean(command.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public int InsertRoom(IRoom room)
        {
            try
            {
                bool roomCountByRoomNumber = GetRoomCountByRoomNumber(room.RoomNumber);

                if (!roomCountByRoomNumber)
                {
                    int result;
                    NpgsqlCommand command = new NpgsqlCommand(
                        "INSERT INTO rooms (room_number, room_type, beds, size, air_conditioning, television, mini_fridge, jacuzzi, wifi) VALUES (@room_number, @room_type, @beds, @size, @air_conditioning, @television, @mini_fridge, @jacuzzi, @wifi)");

                    NpgsqlParameter roomNumberParam = new NpgsqlParameter("@room_number", room.RoomNumber);
                    NpgsqlParameter roomTypeParam = new NpgsqlParameter("@room_type", room.RoomType.ToString());
                    NpgsqlParameter bedsParam = new NpgsqlParameter("@beds", room.Beds);
                    NpgsqlParameter sizeParam = new NpgsqlParameter("@size", room.Size);
                    NpgsqlParameter acParam = new NpgsqlParameter("@air_conditioning", room.AirConditioning);
                    NpgsqlParameter tvParam = new NpgsqlParameter("@television", room.Television);
                    NpgsqlParameter miniFridgeParam = new NpgsqlParameter("@mini_fridge", room.MiniFridge);
                    NpgsqlParameter jacuzziParam = new NpgsqlParameter("@jacuzzi", false);
                    NpgsqlParameter wifiParam = new NpgsqlParameter("@wifi", false);

                    if (room.RoomType == RoomTypeEnum.Standard)
                    {
                        StandardRoom standard = (StandardRoom)room;
                        acParam = new NpgsqlParameter("@air_conditioning", standard.AirConditioning);
                        tvParam = new NpgsqlParameter("@television", standard.Television);
                        miniFridgeParam = new NpgsqlParameter("@mini_fridge", standard.MiniFridge);
                    }
                    else if (room.RoomType == RoomTypeEnum.Executive)
                    {
                        ExecutiveRoom executive = (ExecutiveRoom)room;
                        jacuzziParam = new NpgsqlParameter("@jacuzzi", executive.Jacuzzi);
                    }
                    else if (room.RoomType == RoomTypeEnum.Deluxe)
                    {
                        DeluxeRoom deluxe = (DeluxeRoom)room;
                        wifiParam = new NpgsqlParameter("@wifi", deluxe.Wifi);
                    }

                    result = this.ExecuteQuery(command, roomNumberParam, roomTypeParam, bedsParam,
                        sizeParam, acParam, tvParam, miniFridgeParam, jacuzziParam, wifiParam);

                    return result;
                }
                else
                {
                    MessageBox.Show("Cannot insert room because this room number is already inserted.");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int UpdateRoom(IRoom room)
        {
            int result;
            NpgsqlCommand command = new NpgsqlCommand(
                "UPDATE rooms SET beds = @beds, size = @size, air_conditioning = @air_conditioning, television = @television, mini_fridge = @mini_fridge, jacuzzi = @jacuzzi, wifi = @wifi WHERE id = @id");

            NpgsqlParameter idParam = new NpgsqlParameter("@id", room.Id);
            NpgsqlParameter bedsParam = new NpgsqlParameter("@beds", room.Beds);
            NpgsqlParameter sizeParam = new NpgsqlParameter("@size", room.Size);
            NpgsqlParameter acParam = new NpgsqlParameter("@air_conditioning", room.AirConditioning);
            NpgsqlParameter tvParam = new NpgsqlParameter("@television", room.Television);
            NpgsqlParameter miniFridgeParam = new NpgsqlParameter("@mini_fridge", room.MiniFridge);
            NpgsqlParameter jacuzziParam = new NpgsqlParameter("@jacuzzi", false);
            NpgsqlParameter wifiParam = new NpgsqlParameter("@wifi", false);

            if (room.RoomType == RoomTypeEnum.Standard)
            {
                StandardRoom standard = (StandardRoom)room;
                acParam = new NpgsqlParameter("@air_conditioning", standard.AirConditioning);
                tvParam = new NpgsqlParameter("@television", standard.Television);
                miniFridgeParam = new NpgsqlParameter("@mini_fridge", standard.MiniFridge);
            }
            else if (room.RoomType == RoomTypeEnum.Executive)
            {
                ExecutiveRoom executive = (ExecutiveRoom)room;
                jacuzziParam = new NpgsqlParameter("@jacuzzi", executive.Jacuzzi);
            }
            else if (room.RoomType == RoomTypeEnum.Deluxe)
            {
                DeluxeRoom deluxe = (DeluxeRoom)room;
                wifiParam = new NpgsqlParameter("@wifi", deluxe.Wifi);
            }

            result = ExecuteQuery(command, idParam, bedsParam, 
                sizeParam, acParam, tvParam, miniFridgeParam, jacuzziParam, wifiParam);

            return result;
        }

        public int DeleteRoom(IRoom room)
        {
            try
            {
                bool reservationsByRoomIdCount = GetReservationsByRoomId(room.Id);

                if (!reservationsByRoomIdCount)
                {
                    int result;
                    NpgsqlCommand command = new NpgsqlCommand("DELETE FROM rooms WHERE id = @id");

                    NpgsqlParameter idParam = new NpgsqlParameter("@id", room.Id);

                    result = ExecuteQuery(command, idParam);

                    return result;
                }
                else
                {
                    MessageBox.Show("Cannot remove room since it is bound to a reservation.");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Reservation> GetReservations()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                Reservation reservation = new Reservation();
                string selectQuery = "SELECT * FROM reservation";
                
                using(NpgsqlCommand command = new NpgsqlCommand(selectQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();
                        NpgsqlDataReader reader = command.ExecuteReader();
                        
                        while (reader.Read())
                        {
                            int roomId = (int)reader["room_id"];
                            reservation = new Reservation
                            {
                                Id = (int)reader["id"],
                                RoomId = roomId,
                                Room = GetOneRoom(roomId),
                                Username = (string)reader["username"],
                                StartDate = reader.GetDateTime(3),
                                EndDate = reader.GetDateTime(4)
                            };
                            reservations.Add(reservation);
                        }

                        reader.Close();
                        return reservations;
                    }
                    catch (NpgsqlException npg)
                    {
                        throw new Exception("An error ocurred when accessing database: " + npg.Message);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error when loading reservations: " + ex.Message);
                    }
                }
            }
        }

        public bool GetReservationsByRoomId(int roomId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                string countQuery = "Select COUNT(*) from reservation WHERE room_id = @room_id";

                using (NpgsqlCommand command = new NpgsqlCommand(countQuery, conn))
                {
                    command.Parameters.AddWithValue("@room_id", roomId);

                    try
                    {
                        conn.Open();
                        command.Connection = conn;

                        return Convert.ToBoolean(command.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public int InsertReservation(Reservation reservation)
        {
            int result;
            NpgsqlCommand command = new NpgsqlCommand(
                "INSERT INTO reservation (room_id, username, start_date, end_date) VALUES (@room_id, @username, @start_date, @end_date)");
            
            NpgsqlParameter roomIdParam = new NpgsqlParameter("@room_id", reservation.Room.Id);
            NpgsqlParameter usernameParam = new NpgsqlParameter("@username", reservation.Username);
            NpgsqlParameter startDateParam = new NpgsqlParameter("@start_date", reservation.StartDate);
            NpgsqlParameter endDateParam = new NpgsqlParameter("@end_date", reservation.EndDate);

            result = ExecuteQuery(command, roomIdParam, usernameParam, startDateParam, endDateParam);

            return result;
        }

        public int UpdateReservation(Reservation reservation)
        {
            int result;
            NpgsqlCommand command = new NpgsqlCommand(
                "UPDATE reservation SET room_id = @room_id, username = @username, start_date = @start_date, end_date = @end_date WHERE id = @id");

            NpgsqlParameter reservationIdParam = new NpgsqlParameter("@id", reservation.Id);
            NpgsqlParameter roomIdParam = new NpgsqlParameter("@room_id", reservation.Room.Id);
            NpgsqlParameter usernameParam = new NpgsqlParameter("@username", reservation.Username);
            NpgsqlParameter startDateParam = new NpgsqlParameter("@start_date", reservation.StartDate);
            NpgsqlParameter endDateParam = new NpgsqlParameter("@end_date", reservation.EndDate);

            result = ExecuteQuery(command, reservationIdParam, roomIdParam, usernameParam, startDateParam, endDateParam);

            return result;
        }
        public int DeleteReservation(Reservation reservation)
        {
            int result;
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM reservation WHERE id = @id");

            NpgsqlParameter reservationIdParam = new NpgsqlParameter("@id", reservation.Id);

            result = ExecuteQuery(command, reservationIdParam);

            return result;
        }

        public int ExecuteQuery(NpgsqlCommand command, params NpgsqlParameter[] parameters)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (command)
                    {
                        command.Connection = conn;
                        command.Parameters.AddRange(parameters);
                        int result = command.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
