using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.Reservation.Responses
{
    public class PostReservationResponse:BaseResponse
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
        public int PetCount { get; set; }
        // gunlerle biryerde olan pricedi, normalini propertyden chek goster ekranda idc
        public int PricePerDay { get; set; }
        public int ServiceFee { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }
        public Guid PropertyId { get; set; }
   
        // who's reserving
        public Guid AppUserId { get; set; }
   
        // who ownes it
        public Guid HostId { get; set; }
        // one to one
        //public PropertyReview PropertyReview { get; set; }
        //public GuestReview GuestReview { get; set; }
    }
}
