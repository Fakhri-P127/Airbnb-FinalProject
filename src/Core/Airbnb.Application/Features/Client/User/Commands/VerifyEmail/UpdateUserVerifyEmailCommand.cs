﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.User.Commands.VerifyEmail
{
    public class UpdateUserVerifyEmailCommand:IRequest
    {
        public string Id { get; set; }
        public UpdateUserVerifyEmailCommand(string id)
        {
            Id = id;
        }
    }
}