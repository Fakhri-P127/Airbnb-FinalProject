using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Create
{
    public class CreatePrivacyTypeCommand:IRequest<PrivacyTypeResponse>
    {
        public string Name { get; set; }

    }
}
