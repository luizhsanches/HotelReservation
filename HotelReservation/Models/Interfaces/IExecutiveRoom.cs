using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models.Interfaces
{
    public interface IExecutiveRoom : IRoom
    {
        bool Jacuzzi { get; set; }
    }
}
