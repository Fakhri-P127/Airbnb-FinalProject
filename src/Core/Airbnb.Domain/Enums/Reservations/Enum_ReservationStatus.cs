using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Enums.Reservations
{
    public enum Enum_ReservationStatus
    {
        //reserve olunanlar, vaxtin onemi yoxdu
        Upcoming=1,
        // bu gun ve ya sabah gelecek olanlar
        ArrivingSoon,
        // bu deqiqe tripde olanlar
        CurrentlyHosting,
        // bu gun ve ya sabah check out olanlar
        CheckingOut,
        // reservation finished
        ReservationFinished,
        // cancelled olan reservationlar
        ReservationCancelled
    }
}
