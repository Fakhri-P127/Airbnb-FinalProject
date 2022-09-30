using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using LinqKit;
using MediatR;

namespace Airbnb.Application.Features.Client.Hosts.Queries.GetAll
{
    public class GetAllHostQueryHandler : IRequestHandler<GetAllHostQuery, List<GetHostResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAllHostQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<GetHostResponse>> Handle(GetAllHostQuery request, CancellationToken cancellationToken)
        {
            ExpressionStarter<Host> filters = FilterRequest(request);
            List<Host> hosts = await _unit.HostRepository.GetAllAsync(filters, request.Parameters, false,
                HostHelper.AllHostIncludes());

            List<GetHostResponse> responses = _mapper.Map<List<GetHostResponse>>(hosts);
            if (responses is null) throw new Exception("Internal server error");
            return responses;

        }

        private static ExpressionStarter<Host> FilterRequest(GetAllHostQuery request)
        {
            ExpressionStarter<Host> predicate = PredicateBuilder.New<Host>(true);
            if (request.Parameters.AppUserId.HasValue) predicate = predicate
                    .And(x => x.AppUserId == request.Parameters.AppUserId);
            if (request.Parameters.Status.HasValue)
                predicate = predicate.And(x =>x.Status == request.Parameters.Status);
            if (request.Expression != null) predicate = predicate.And(request.Expression);
            
            return ExpressionHelpers<Host>.FilteredPredicateOrIfNoFilterReturnNull(predicate);
        }

        
    }
}
