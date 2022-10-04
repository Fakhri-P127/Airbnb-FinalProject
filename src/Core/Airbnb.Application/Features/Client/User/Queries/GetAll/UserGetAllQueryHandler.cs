using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Application.Features.Client.User.Queries.GetAll
{
    public class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, List<UserResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly CustomUserManager<AppUser> _userManager;

        public UserGetAllQueryHandler(IUnitOfWork unit,IMapper mapper,CustomUserManager<AppUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<List<UserResponse>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
        {
            ExpressionStarter<AppUser> filters = FilterRequest(request);
            IQueryable<AppUser> query = filters is not null ?
               _userManager.Users.Where(filters) : _userManager.Users.AsQueryable();
            List<AppUser> users = await query?
                .SetIncludes(AppUserHelper.AllUserIncludes()).AsNoTrackingWithIdentityResolution()/*.AsSplitQuery()*/
                .Skip((request.Parameters.PageNumber - 1) * request.Parameters.PageSize)
                .Take(request.Parameters.PageSize).ToListAsync(cancellationToken);

            List<UserResponse> responses = _mapper.Map<List<UserResponse>>(users);
            AddVerifications(responses, users);
            return responses;
        }

        private ExpressionStarter<AppUser> FilterRequest(UserGetAllQuery request)
        {
            ExpressionStarter<AppUser> filters = PredicateBuilder.New<AppUser>(true);
            if (!string.IsNullOrWhiteSpace(request.Parameters.Firstname)) filters = filters
                    .And(x => x.Firstname.Contains(request.Parameters.Firstname));
            if (!string.IsNullOrWhiteSpace(request.Parameters.Lastname)) filters = filters
                    .And(x => x.Lastname.Contains(request.Parameters.Lastname));
            if (request.Parameters.GenderId.HasValue) filters = filters
                    .And(x => x.GenderId == request.Parameters.GenderId);

            filters = FilterByDateOfBirth(request, filters);
            filters = FilterPerProfilePicture(request, filters);
            filters = FilterMinAndMaxParameters(request, filters);
            filters = FilterLanguages(request, filters);

            if (request.Expression is not null) filters = filters
                    .And(request.Expression);
            ExpressionHelpers<AppUser>.FilteredPredicateOrIfNoFilterReturnNull(filters);
            return filters;
        }

        private static ExpressionStarter<AppUser> FilterByDateOfBirth(UserGetAllQuery request, ExpressionStarter<AppUser> filters)
        {
            if (request.Parameters.MinDateOfBirth.HasValue) filters = filters
                    .And(x => x.DateOfBirth >= request.Parameters.MinDateOfBirth);
            if (request.Parameters.MaxDateOfBirth.HasValue) filters = filters
                    .And(x => x.DateOfBirth <= request.Parameters.MaxDateOfBirth);
            return filters;
        }

        private static ExpressionStarter<AppUser> FilterPerProfilePicture(UserGetAllQuery request, ExpressionStarter<AppUser> filters)
        {
            if (request.Parameters.HasProfilPicture.HasValue && request.Parameters.HasProfilPicture is true)
                filters = filters.And(x => x.ProfilPicture != null);
            if (request.Parameters.HasProfilPicture.HasValue && request.Parameters.HasProfilPicture is false)
                filters = filters.And(x => x.ProfilPicture == null);
            return filters;
        }

        private ExpressionStarter<AppUser> FilterLanguages(UserGetAllQuery request, ExpressionStarter<AppUser> filters)
        {
            if (request.Parameters.LanguageCodes is not null && request.Parameters.LanguageCodes.Any())
            {
                List<Language> allLanguages =  _unit.LanguageRepository
                    .GetAllAsync(null, null).GetAwaiter().GetResult();
                request.Parameters.LanguageCodes.ForEach(languageCode =>
                {
                    // eger bele bir dil varsa filter etsin yoxdusa boshuna edib empty list qaytarmasin
                    if (allLanguages.Any(x => x.LanguageCode == languageCode))
                        filters = filters
                   .And(x => x.AppUserLanguages.Any(x => x.Language.LanguageCode == languageCode));
                });
            }

            return filters;
        }

        private static ExpressionStarter<AppUser> FilterMinAndMaxParameters(UserGetAllQuery request, ExpressionStarter<AppUser> filters)
        {
            if (request.Parameters.MinCountForReviewsByYou.HasValue) filters = filters
                    .And(x => x.ReviewsByYou.Count >= request.Parameters.MinCountForReviewsByYou);
            if (request.Parameters.MaxCountForReviewsByYou.HasValue) filters = filters
                    .And(x => x.ReviewsByYou.Count <= request.Parameters.MaxCountForReviewsByYou);
            if (request.Parameters.MinCountForReviewsAboutYou.HasValue) filters = filters
                  .And(x => x.ReviewsAboutYou.Count >= request.Parameters.MinCountForReviewsAboutYou);
            if (request.Parameters.MaxCountForReviewsAboutYou.HasValue) filters = filters
                    .And(x => x.ReviewsAboutYou.Count <= request.Parameters.MaxCountForReviewsAboutYou);

            if (request.Parameters.MinCountForReservationsYouMade.HasValue) filters = filters
                   .And(x => x.ReservationsYouMade.Count >= request.Parameters.MinCountForReservationsYouMade);
            if (request.Parameters.MaxCountForReservationsYouMade.HasValue) filters = filters
                    .And(x => x.ReservationsYouMade.Count <= request.Parameters.MinCountForReservationsYouMade);
            return filters;
        }

        // bu funksiyani silmekde olar, bir sheye yaramir choxda, esas vizual olaraq gorsetmek uchun idi
        private static List<UserResponse> AddVerifications(List<UserResponse> responses, List<AppUser> users)
        {
            foreach (AppUser user in users)
            {
                UserResponse response = responses.FirstOrDefault(x => x.Id == user.Id);
                if (response is null) throw new UserNotFoundValidationException();

                if (user.EmailConfirmed) response.Verifications.Add("Email verified");
                if (user.PhoneNumberConfirmed) response.Verifications.Add("Phone number verified");

            }
            return responses;
        }
    }
}
