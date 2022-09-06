using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Features.Admin.AirCovers.Commands.Create;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Mapping
{
    public class AirCoverMappings:Profile
    {
        public AirCoverMappings()
        {
            CreateMap<AirCover, AirCoverResponse>();
            CreateMap<CreateAirCoverCommand, AirCover>();
        }
    }
}
