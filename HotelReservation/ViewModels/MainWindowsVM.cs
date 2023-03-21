using HotelReservation.Models;
using HotelReservation.Models.DatabaseContext;
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
        private ObservableCollection<IRoom> roomList;
        private ObservableCollection<Reservation> reservationList;
        private int roomTypeItem;
        private IRoom selectedRoom;
        private Reservation selectedReservation;

        public ObservableCollection<Reservation> ReservationList
        {
            get { return reservationList; }
        }

        public ObservableCollection<IRoom> RoomList
        {
            get { return roomList; }
        }

        public int RoomTypeItem
        {
            get { return roomTypeItem; }
            set
            {
                roomTypeItem = value;
                Notifica(nameof(RoomTypeItem));
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
                roomList = database.GetRooms();
                reservationList = new ObservableCollection<Reservation>();
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
                IRoom room = Room.selectRoomType(RoomTypeItem);
                Window screen = Room.selectRoomWindow(RoomTypeItem);
                Enum roomTypeEnum = Room.selectRoomTypeEnum(RoomTypeItem);
                
                screen.DataContext = room;
                bool? verifica = screen.ShowDialog();

                if (verifica.HasValue && verifica.Value)
                {
                    try
                    {
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
                try
                {
                    if (SelectedRoom != null)
                    {
                        IRoom room = SelectedRoom.Clone();
                        Window screen = Room.selectRoomWindow(RoomTypeItem);
                        Enum roomTypeEnum = Room.selectRoomTypeEnum(RoomTypeItem);

                        screen.DataContext = room;
                        bool? verifica = screen.ShowDialog();

                        if (verifica.HasValue && verifica.Value)
                        {
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
                                throw new Exception("An error ocurred");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("A room should be selected in the listview.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }, (object _) => RoomTypeItem != (int)RoomTypeEnum.All);

            RemoveRoom = new RelayCommand((object _) =>
            {
                try
                {
                    int res = database.DeleteRoom(SelectedRoom.RoomNumber);

                    if (res == 1)
                    {
                        roomList.Remove(SelectedRoom);
                        RoomTypeItem = (int)RoomTypeEnum.All;

                        MessageBox.Show("Room deleted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    //insert db
                    string text = screen.cbRooms.Text;
                    IRoom room = roomList.FirstOrDefault(r => r.RoomNumber.ToString() == text);
                    newReservation.Room = room;
                    reservationList.Add(newReservation);
                }
            });

            EditReservation = new RelayCommand((object _) =>
            {
                try
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
                            //update db
                            SelectedReservation.CopyReservation(reservationToUpdate);
                            Notifica(nameof(ReservationList)); //don't update-must check
                        }
                    }
                    else
                    {
                        throw new Exception("A reservation should be selected in the listview.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            RemoveReservation = new RelayCommand((object _) =>
            {
                try
                {
                    //delete db
                    reservationList.Remove(SelectedReservation);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

        }
    }
}
