using Airbnb.Application.Features.Client.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Authentication.Queries.Login
{
    public class LoginQuery:IRequest<AuthenticationResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
