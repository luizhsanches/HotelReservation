using HotelReservation.Models.Rooms;
using HotelReservation.Models.Validators;
using HotelReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Models.Reservations;
using HotelReservation.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationTests
{
    public class ReservationValidatorTests
    {
        private ReservationValidator reservationValidator;
        private IRoom room;

        [SetUp]
        public void Setup()
        {
            reservationValidator = new ReservationValidator();
            room = new StandardRoom();
        }

        [Test]
        public void Validate_ValidMovie_NoExceptionThrown()
        {
            Reservation reservation = new Reservation
            {
                RoomId = 1,
                Room = room,
                Username = "Luiz Sanches",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };

            // Act
            TestDelegate action = () => reservationValidator.Validate(reservation);

            // Assert
            Assert.DoesNotThrow(action);
        }

        [Test]
        public void ValidateUsernameIsNull_ThrowsArgumentException()
        {
            Reservation reservation = new Reservation
            {
                RoomId = 1,
                Room = room,
                Username = null,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };

            TestDelegate action = () => reservationValidator.Validate(reservation);

            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void ValidateUsernameIsLessThanThreeCharacters_ThrowsArgumentException()
        {
            Reservation reservation = new Reservation
            {
                RoomId = 1,
                Room = room,
                Username = "Lu",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };

            TestDelegate action = () => reservationValidator.Validate(reservation);

            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void ValidateStartTimeIsGreaterThanEndTime_ThrowsArgumentException()
        {
            Reservation reservation = new Reservation
            {
                RoomId = 1,
                Room = room,
                Username = "Lu",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now
            };

            TestDelegate action = () => reservationValidator.Validate(reservation);

            Assert.Throws<ArgumentException>(action);
        }
    }
}
