using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models.Interfaces
{
    public interface IDeluxeRoom : IExecutiveRoom
    {
        bool Wifi { get; set; }
    }
}
