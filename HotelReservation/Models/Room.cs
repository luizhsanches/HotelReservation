﻿using HotelReservation.ViewModels.VmUtils;
using HotelReservation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservation.Models
{
    public static class Room
    {
        public static IRoomType selectRoomType(int roomTypeItem)
        {
            if (roomTypeItem == (int)RoomTypeEnum.Standard)
            {
                return new StandardRoom();
            }
            else if (roomTypeItem == (int)RoomTypeEnum.Executive)
            {
                return new ExecutiveRoom();
            }
            else if (roomTypeItem == (int)RoomTypeEnum.Deluxe)
            {
                return new DeluxeRoom();
            }

            return null;
        }

        public static Window selectRoomWindow(int roomTypeItem)
        {
            if (roomTypeItem == (int)RoomTypeEnum.Standard)
            {
                return new StandardView();
            }
            else if (roomTypeItem == (int)RoomTypeEnum.Executive)
            {
                return new ExecutiveView();
            }
            else if (roomTypeItem == (int)RoomTypeEnum.Deluxe)
            {
                return new DeluxeView();
            }
            return null;
        }
    }
}