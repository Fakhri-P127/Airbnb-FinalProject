using Airbnb.Domain.Entities.Base;
using Airbnb.Domain.Entities.AppUserRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.PropertyRelated
{
    public class PropertyReview : BaseEntity
    {
        //one to one rating ile
        public string Text { get; set; }
        public float OverallScore { get; set; }
        public float CleanlinessScore { get; set; }
        public float CommunicationScore { get; set; }
        public float CheckInScore { get; set; }
        public float AccuracyScore { get; set; }
        public float LocationScore { get; set; }
        public float ValueScore { get; set; }
        // check ele ki AppUserId bu property uchun edilen bookingde Id si var :# 
        // bu appUser reserve i edendi
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        // guestin etdiyi reservation deki prop Id bunla ust uste dushurse icaze ver
        //public Guid PropertyId { get; set; }
        //public Property Property { get; set; }
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public Guid HostId { get; set; }
        public Host Host { get; set; }
        //public Guid PropertyId { get; set; }
        //public Property Property { get; set; }


    }
}
