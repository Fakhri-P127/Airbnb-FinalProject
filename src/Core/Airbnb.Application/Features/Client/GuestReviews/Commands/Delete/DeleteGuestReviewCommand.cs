using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.GuestReviews.Commands.Delete
{
    public class DeleteGuestReviewCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeleteGuestReviewCommand(Guid id)
        {
            Id = id;
        }
    }
}
