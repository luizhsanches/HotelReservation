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
    public class PostgresDB : IDbContext
    {
        private readonly string connectionString;

        public PostgresDB(bool isTestDb = false)
        {
            if (isTestDb)
            {
                connectionString = "Server=localhost;Port=5432;Database=hotel_reservation;User Id=postgres;Password=1234;Integrated Security=true;";
            }
            else
            {
                connectionString = "Server=localhost;Port=5432;Database=hotel_reservation;User Id=postgres;Password=1234;Integrated Security=true;";
            }


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
            catch (InvalidOperationException io)
            {
                throw new Exception("Invalid data." + io.Message);
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
                            _ = Enum.TryParse(reader.GetString(1), out RoomTypeEnum roomType);
                            if (roomType == RoomTypeEnum.Standard)
                            {
                                room = new StandardRoom
                                {
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

        public int InsertRoom(IRoom room)
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
                jacuzziParam.ParameterName = "@jacuzzi";
                jacuzziParam.Value = executive.Jacuzzi;
            }
            else if (room.RoomType == RoomTypeEnum.Deluxe)
            {
                DeluxeRoom deluxe = (DeluxeRoom)room;
                wifiParam.ParameterName = "@wifi";
                wifiParam.Value = deluxe.Wifi;
            }

            result = this.ExecuteQuery(command, roomNumberParam, roomTypeParam, bedsParam, sizeParam, acParam, tvParam, miniFridgeParam, jacuzziParam, wifiParam);
            
            return result;
        }
        

        public int UpdateRoom(IRoom room)
        {
            int result;
            NpgsqlCommand command = new NpgsqlCommand(
                "UPDATE rooms SET room_type = @room_type, beds = @beds, size = @size, air_conditioning = @air_conditioning, television = @television, mini_fridge = @mini_fridge, jacuzzi = @jacuzzi, wifi = @wifi WHERE room_number = @room_number");

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
                jacuzziParam.ParameterName = "@jacuzzi";
                jacuzziParam.Value = executive.Jacuzzi;
            }
            else if (room.RoomType == RoomTypeEnum.Deluxe)
            {
                DeluxeRoom deluxe = (DeluxeRoom)room;
                wifiParam.ParameterName = "@wifi";
                wifiParam.Value = deluxe.Wifi;
            }

            result = ExecuteQuery(command, roomNumberParam, roomTypeParam, bedsParam, sizeParam, acParam, tvParam, miniFridgeParam, jacuzziParam, wifiParam);

            return result;
        }

        public int DeleteRoom(int roomId)
        {
            int result;
            NpgsqlCommand command = new NpgsqlCommand(
                "DELETE FROM rooms WHERE room_number = @room_number");

            NpgsqlParameter roomNumberParam = new NpgsqlParameter("@room_number", roomId);

            result = ExecuteQuery(command, roomNumberParam);

            return result;

        }

        public ObservableCollection<Reservation> GetReservations()
        {
            throw new NotImplementedException();
        }

        public int InsertReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public int UpdateReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }
        public int DeleteReservation(int reservationId)
        {
            throw new NotImplementedException();
        }
    }
}
