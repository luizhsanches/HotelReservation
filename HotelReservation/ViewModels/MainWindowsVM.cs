using HotelReservation.Models;
using HotelReservation.Models.Reservations;
using HotelReservation.Models.DatabaseContext;
using HotelReservation.Models.Interfaces;
using HotelReservation.Models.Validators;
using HotelReservation.ViewModels.VmUtils;
using HotelReservation.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelReservation.ViewModels
{
    public class MainWindowsVM : BaseNotifier
    {
        private readonly IDbContext database;
        private RoomValidator roomValidator;
        private ReservationValidator reservationValidator;
        private ObservableCollection<IRoom> roomList;
        private ObservableCollection<Reservation> reservationList;
        private int roomTypeItem;
        private IRoom selectedRoom;
        private Reservation selectedReservation;

        public IEnumerable<IRoom> RoomList
        {
            get
            {
                if (RoomTypeItem == (int)RoomTypeEnum.All)
                {
                    return roomList;
                }
                else
                {
                    return roomList.Where(room => room.RoomType == (RoomTypeEnum)Room.SelectRoomTypeEnum(RoomTypeItem));
                }
            }
        }

        public ObservableCollection<Reservation> ReservationList
        {
            get { return reservationList; }
        }

        public int RoomTypeItem
        {
            get { return roomTypeItem; }
            set
            {
                roomTypeItem = value;
                Notifica(nameof(RoomList));
            }
        }

        public IRoom SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                if (selectedRoom != value)
                {
                    selectedRoom = value;
                    Notifica(nameof(SelectedRoom));
                }
            }
        }

        public Reservation SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                if (selectedReservation != value)
                {
                    selectedReservation = value;
                    Notifica(nameof(SelectedReservation));
                }
            }
        }

        public ICommand AddRoom { get; private set; }
        public ICommand EditRoom { get; private set; }
        public ICommand RemoveRoom { get; private set; }
        public ICommand AddReservation { get; private set;}
        public ICommand EditReservation { get; private set; }
        public ICommand RemoveReservation { get; private set; }

        public MainWindowsVM()
        {
            database = new PostgresDB();
            try
            {
                roomValidator = new RoomValidator();
                reservationValidator = new ReservationValidator();
                roomList = database.GetRooms();
                reservationList = database.GetReservations();
                RoomTypeItem = (int)RoomTypeEnum.All;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            InitializeCommands();
        }

        public void InitializeCommands()
        {
            AddRoom = new RelayCommand((object _) =>
            {                
                IRoom room = Room.SelectRoomType(RoomTypeItem);
                Window screen = Room.SelectRoomWindow(RoomTypeItem);
                Enum roomTypeEnum = Room.SelectRoomTypeEnum(RoomTypeItem);
                
                screen.DataContext = room;
                bool? verifica = screen.ShowDialog();

                if (verifica.HasValue && verifica.Value)
                {
                    try
                    {
                        roomValidator.Validate(room);
                        room.RoomType = (RoomTypeEnum)roomTypeEnum;
                        int res = database.InsertRoom(room);
                        if (res == 1)
                        {
                            roomList.Add(room);
                            MessageBox.Show("Room added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            throw new Exception("An error ocurred");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }, (object _) => RoomTypeItem != (int)RoomTypeEnum.All);

            EditRoom = new RelayCommand((object _) =>
            {
                if (SelectedRoom != null)
                {
                    IRoom room = SelectedRoom.Clone();
                    Window screen = Room.SelectRoomWindow(RoomTypeItem);
                    Enum roomTypeEnum = Room.SelectRoomTypeEnum(RoomTypeItem);

                    screen.DataContext = room;
                    bool? verifica = screen.ShowDialog();

                    if (verifica.HasValue && verifica.Value)
                    {
                        try
                        {
                            roomValidator.Validate(room);
                            room.RoomType = (RoomTypeEnum)roomTypeEnum;
                            int res = database.UpdateRoom(room);
                            if (res == 1)
                            {
                                SelectedRoom.CopyRoom(room);
                                Notifica(nameof(RoomList));

                                MessageBox.Show("Room updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("An error ocurred.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("A room should be selected in the listview.");
                }

            }, (object _) => RoomTypeItem != (int)RoomTypeEnum.All);

            RemoveRoom = new RelayCommand((object _) =>
            {
                if (SelectedRoom != null)
                {
                    try
                    {
                        int res = database.DeleteRoom(SelectedRoom);

                        if (res == 1)
                        {
                            roomList.Remove(SelectedRoom);
                            RoomTypeItem = (int)RoomTypeEnum.All;

                            MessageBox.Show("Room deleted!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            MessageBox.Show("An error ocurred");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("A room should be selected in the listview.");
                }
            });

            AddReservation = new RelayCommand((object _) =>
            {
                Reservation newReservation = new Reservation();

                ReservationView screen = new ReservationView();
                screen.DataContext = newReservation;
                screen.cbRooms.ItemsSource = roomList;
                screen.cbRooms.DisplayMemberPath = "RoomNumber";
                bool? verifica = screen.ShowDialog();

                if (verifica.HasValue && verifica.Value)
                {
                    try
                    {
                        string text = screen.cbRooms.Text;
                        IRoom room = roomList.FirstOrDefault(r => r.RoomNumber.ToString() == text);
                        newReservation.Room = room;

                        reservationValidator.Validate(newReservation);
                        int res = database.InsertReservation(newReservation);

                        if (res == 1)
                        {
                            reservationList.Add(newReservation);

                            MessageBox.Show("Reservation added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("An error ocurred");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });

            EditReservation = new RelayCommand((object _) =>
            {
                if (SelectedReservation != null)
                {
                    Reservation reservationToUpdate = SelectedReservation.Clone();
                    ReservationView screen = new ReservationView();
                    screen.DataContext = reservationToUpdate;
                    screen.cbRooms.ItemsSource = roomList;
                    screen.cbRooms.DisplayMemberPath = "RoomNumber";
                    screen.cbRooms.SelectedValue = reservationToUpdate.Room.RoomNumber.ToString();
                    bool? verifica = screen.ShowDialog();

                    if (verifica.HasValue && verifica.Value)
                    {
                        try
                        {
                            reservationValidator.Validate(reservationToUpdate);
                            int res = database.UpdateReservation(reservationToUpdate);

                            if (res == 1)
                            {
                                SelectedReservation.CopyReservation(reservationToUpdate);
                                reservationList = database.GetReservations();
                                Notifica(nameof(ReservationList));

                                MessageBox.Show("Reservation updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("An error ocurred");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("A reservation should be selected in the listview.");
                }
            });

            RemoveReservation = new RelayCommand((object _) =>
            {
                if (SelectedReservation != null)
                {
                    try
                    {
                        int res = database.DeleteReservation(SelectedReservation);
                        if (res == 1)
                        {
                            reservationList.Remove(SelectedReservation);
                            MessageBox.Show("Reservation deleted!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            MessageBox.Show("An error ocurred");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("A reservation should be selected in the listview.");
                }
            });

        }
    }
}
