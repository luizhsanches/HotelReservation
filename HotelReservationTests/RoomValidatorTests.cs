using HotelReservation.Models;
using HotelReservation.Models.Interfaces;
using HotelReservation.Models.Reservations;
using HotelReservation.Models.Rooms;
using HotelReservation.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationTests
{
    [TestFixture]
    public class RoomValidatorTests
    {
        private RoomValidator roomValidator;
        
        [SetUp]
        public void Setup()
        {
            roomValidator = new RoomValidator();
        }

        [Test]
        public void ValidateValidRoom_NoExceptionThrown()
        {
            StandardRoom stdRoom = new StandardRoom
            {
                RoomNumber = 1,
                RoomType = RoomTypeEnum.Standard,
                Beds = 1,
                Size = 1,
                AirConditioning = false,
                Television = false,
                MiniFridge = false
            };

            // Act
            TestDelegate action = () => roomValidator.Validate(stdRoom);

            // Assert
            Assert.DoesNotThrow(action);
        }

        [Test]
        public void ValidateRoomNumberIsZero_ThrowsArgumentException()
        {
            StandardRoom stdRoom = new StandardRoom
            {
                RoomNumber = 0,
                RoomType = RoomTypeEnum.Standard,
                Beds = 1,
                Size = 1,
                AirConditioning = false,
                Television = false,
                MiniFridge = false
            };

            TestDelegate action = () => roomValidator.Validate(stdRoom);

            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void ValidateBedsIsZero_ThrowsArgumentException()
        {
            StandardRoom stdRoom = new StandardRoom
            {
                RoomNumber = 1,
                RoomType = RoomTypeEnum.Standard,
                Beds = 0,
                Size = 1,
                AirConditioning = false,
                Television = false,
                MiniFridge = false
            };

            TestDelegate action = () => roomValidator.Validate(stdRoom);

            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void ValidateSizeIsZero_ThrowsArgumentException()
        {
            StandardRoom stdRoom = new StandardRoom
            {
                RoomNumber = 0,
                RoomType = RoomTypeEnum.Standard,
                Beds = 1,
                Size = 0,
                AirConditioning = false,
                Television = false,
                MiniFridge = false
            };

            TestDelegate action = () => roomValidator.Validate(stdRoom);

            Assert.Throws<ArgumentException>(action);
        }
    }
}