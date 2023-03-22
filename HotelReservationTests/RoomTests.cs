using HotelReservation.Models;
using HotelReservation.Models.Interfaces;
using HotelReservation.Models.Rooms;
using HotelReservation.Models.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationTests
{
    public class RoomTests
    {
        private IRoom stdRoom;
        private IRoom execRoom;
        private IRoom dlxRoom;

        [SetUp]
        public void Setup()
        {
            stdRoom = new StandardRoom();
            execRoom = new ExecutiveRoom();
            dlxRoom = new DeluxeRoom();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void SelectRoomTypeAndReturnCorrectRoom(int input)
        {
            IRoom roomTest = Room.SelectRoomType(input);

            if (input == 1)
            {
                Assert.That(roomTest.GetType(), Is.EqualTo(stdRoom.GetType()));
            }
            else if (input == 2)
            {
                Assert.That(roomTest.GetType(), Is.EqualTo(execRoom.GetType()));
            }
            else if (input == 3)
            {
                Assert.That(roomTest.GetType(), Is.EqualTo(dlxRoom.GetType()));
            }
            else
            {
                Assert.That(roomTest, Is.Null);
            }
        }
    }
}
