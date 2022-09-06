using Airbnb.Application.Contracts.v1.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses
{
    public class AirCoverResponse:BaseResponse
    {
        public string Logo { get; set; }
        public string Title { get; set; }
        public string BookingProtectionGuarantee { get; set; }
        public string CheckInGuarantee { get; set; }
        public string GetWhatYouBookedGuarantee { get; set; }
        public string FullDaySafetyLine { get; set; }
        public string FindMore { get; set; }

    }
}
