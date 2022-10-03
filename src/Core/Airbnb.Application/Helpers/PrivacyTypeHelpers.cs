using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Helpers
{
    public static class PrivacyTypeHelpers
    {
        public async static Task<PrivacyTypeResponse> ReturnResponse(PrivacyType privacyType,
          IUnitOfWork _unit, IMapper _mapper)
        {
            privacyType = await _unit.PrivacyTypeRepository.GetByIdAsync(privacyType.Id, null,false,
                "Properties");
            PrivacyTypeResponse response = _mapper.Map<PrivacyTypeResponse>(privacyType);
            //if (response is null) throw new Exception("Internal server error");
            return response;
        }
       
    }
}
