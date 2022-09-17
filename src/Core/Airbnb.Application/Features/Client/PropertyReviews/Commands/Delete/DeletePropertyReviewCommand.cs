using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.PropertyReviews.Commands.Delete
{
    public class DeletePropertyReviewCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeletePropertyReviewCommand(Guid id)
        {
            Id = id;
        }
    }
}
