using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Update
{
    public class UpdateAirCoverCommand:IRequest<AirCoverResponse>
    {
        public Guid Id { get; set; }
        public string Logo { get; set; }
        public string Title { get; set; }
        public string BookingProtectionGuarantee { get; set; }
        public string CheckInGuarantee { get; set; }
        public string GetWhatYouBookedGuarantee { get; set; }
        public string FullDaySafetyLine { get; set; }
        public string FindMore { get; set; }
      
    }
}
