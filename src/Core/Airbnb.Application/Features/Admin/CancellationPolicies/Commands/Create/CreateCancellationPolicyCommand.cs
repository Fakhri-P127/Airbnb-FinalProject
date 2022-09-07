﻿using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Create
{
    public class CreateCancellationPolicyCommand:IRequest<CancellationPolicyResponse>
    {
        public string Name { get; set; }
        public string FullRefund { get; set; }
        public string PartialRefund { get; set; }
        public string NoRefund { get; set; }

    }
}
