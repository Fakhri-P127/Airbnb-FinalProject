using Airbnb.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.PropertyRelated
{
    public class AirCover:BaseEntity
    {
        public string Logo { get; set; }
        public string Title { get; set; }
        public string BookingProtectionGuarantee { get; set; }
        public string CheckInGuarantee { get; set; }
        public string GetWhatYouBookedGuarantee { get; set; }
        public string FullDaySafetyLine { get; set; }
        public string FindMore { get; set; }
        //settingsde bunu yarat
        //public string FindMoreLink { get; set; }
    }
}
