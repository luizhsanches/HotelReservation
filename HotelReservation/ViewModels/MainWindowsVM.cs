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
        private ObservableCollection<IRoomType> roomList;
        private ObservableCollection<Reservation> reservationList;
        private int roomTypeItem;

        public IEnumerable<Reservation> ReservationList {
            get { return reservationList; }
        }

        public ObservableCollection<IRoomType> RoomList
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

        public ICommand AddRoom { get; private set; }
        public ICommand EditRoom { get; private set; }
        public ICommand RemoveRoom { get; private set; }
        public ICommand AddReservation { get; private set;}
        public ICommand EditReservation { get; private set; }
        public ICommand RemoveReservation { get; private set; }

        public MainWindowsVM()
        {
            reservationList = new ObservableCollection<Reservation>();
            roomList = new ObservableCollection<IRoomType>();
            RoomTypeItem = (int)RoomTypeEnum.All;
            //BuildRoomList();
            InitializeCommands();
        }

        public void InitializeCommands()
        {
            AddRoom = new RelayCommand((object _) =>
            { 
                IRoomType room = Room.selectRoomType(roomTypeItem);
                Window screen = Room.selectRoomWindow(roomTypeItem);

                screen.DataContext = room;
                bool? verifica = screen.ShowDialog();
                if (verifica.HasValue && verifica.Value)
                {
                    roomList.Add(room);
                    MessageBox.Show("Ok");
                }
            });

            AddReservation = new RelayCommand((object _) =>
            {
                Reservation newReservation = new Reservation();

                ReservationView screen = new ReservationView();
                screen.DataContext = newReservation;
                screen.cbRooms.ItemsSource = roomList;
                bool? verifica = screen.ShowDialog();

                if (verifica.HasValue && verifica.Value)
                {
                    string text = screen.cbRooms.Text;
                    //Room room = roomList.FirstOrDefault(r => r.RoomNumber.ToString() == text);
                    //newReservation.Room = room;
                    reservationList.Add(newReservation);
                }
            });

        }

        /*public void BuildRoomList()
        {
            roomList = new ObservableCollection<IRoom>();
            for (int i = 1; i < 11; i++)
            {
                if (i <= 4)
                {
                    roomList.Add(new Room(i, RoomTypeEnum.Executive));
                }
                else if (i > 4 && i <= 7)
                {
                    roomList.Add(new Room(i, RoomTypeEnum.Deluxe));
                }
                else
                {
                    roomList.Add(new Room(i, RoomTypeEnum.Presidential));
                }
            }
        }*/
    }
}
