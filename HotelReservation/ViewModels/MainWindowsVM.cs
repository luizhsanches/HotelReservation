using HotelReservation.Models;
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
            reservationList = new ObservableCollection<Reservation>();
            roomList = new ObservableCollection<IRoom>();
            RoomTypeItem = (int)RoomTypeEnum.All;
            InitializeCommands();
        }

        public void InitializeCommands()
        {
            AddRoom = new RelayCommand((object _) =>
            {
                IRoom room = Room.selectRoomType(RoomTypeItem);
                Window screen = Room.selectRoomWindow(RoomTypeItem);

                screen.DataContext = room;
                bool? verifica = screen.ShowDialog();

                if (verifica.HasValue && verifica.Value)
                {
                    //insert db
                    roomList.Add(room);
                    MessageBox.Show("Ok");
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

                        screen.DataContext = room;
                        bool? verifica = screen.ShowDialog();

                        if (verifica.HasValue && verifica.Value)
                        {
                            //update db
                            SelectedRoom.CopyRoom(room);

                            MessageBox.Show("Ok");
                        }
                    }
                    else
                    {
                        throw new Exception("A room should be selected in the listview.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }, (object _) => RoomTypeItem != (int)RoomTypeEnum.All);

            RemoveRoom = new RelayCommand((object _) =>
            {
                try
                {
                    //delete db
                    roomList.Remove(SelectedRoom);
                    RoomTypeItem = (int)RoomTypeEnum.All;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
