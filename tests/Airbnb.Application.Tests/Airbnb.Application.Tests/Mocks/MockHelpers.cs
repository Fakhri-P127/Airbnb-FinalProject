using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Features.Client.Reservations.Commands.Create;
using Airbnb.Domain.Entities.AppUserRelated.CustomFrameworkClasses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Linq.Expressions;

namespace Airbnb.Application.Tests.Mocks
{
    public static class MockHelpers
    {
        public static Mock<CustomUserManager<TUser>> MockUserManager<TUser>(List<TUser> users) where TUser : CustomIdentityUser
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<CustomUserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => users.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success)
                .Callback<TUser>(user=>users.Remove(user));
            mgr.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
               .ReturnsAsync(users.Last());//testleri sonuncu ile edirem deye ele bunu cagira bilerem

            return mgr;
        }
        public static Mock<IUnitOfWork> MockedUnitOfWork(List<Reservation> _reservations,
            CreateReservationCommand command,Property _property,IMapper _mapper) 
        {
            var mgr = new Mock<IUnitOfWork>();

            #region predicates
            Func<Reservation, bool> checkInPredicate = x => x.CheckInDate <= command.CheckInDate
                && x.CheckOutDate >= command.CheckInDate;
            Func<Reservation, bool> checkOutPredicate = x => x.CheckInDate <= command.CheckOutDate
                            && x.CheckOutDate >= command.CheckOutDate;
            Func<Reservation, bool> containsOccupiedDatePredicate = x =>
            x.CheckInDate >= command.CheckInDate && x.CheckInDate <= command.CheckOutDate;
            #endregion
            #region mock for propertyrepo
            mgr.Setup(x => x.PropertyRepository.GetByIdAsync(command.PropertyId,
                It.IsAny<Expression<Func<Property, bool>>>(), true, "Host", "PropertyImages"))
                .ReturnsAsync(_property);
            #endregion
            #region unit setups for reservations
            //checkin
            mgr.Setup(x => x.ReservationRepository
                .GetSingleAsync(x => x.CheckInDate <= command.CheckInDate
                && x.CheckOutDate >= command.CheckInDate, false))
                .ReturnsAsync(_reservations.FirstOrDefault(checkInPredicate));
            //checkout
            mgr.Setup(x => x.ReservationRepository
              .GetSingleAsync(x => x.CheckInDate <= command.CheckOutDate
                            && x.CheckOutDate >= command.CheckOutDate, false))
              .ReturnsAsync(_reservations.FirstOrDefault(checkOutPredicate));
            //contains occupied date
            mgr.Setup(x => x.ReservationRepository
            .GetAllAsync(x => x.CheckInDate >= command.CheckInDate && x.CheckInDate <= command.CheckOutDate,
            null, false))
                .ReturnsAsync(_reservations.Where(containsOccupiedDatePredicate).ToList());

            Reservation reservation = _mapper.Map<Reservation>(command);
            reservation.Property = _property;
            mgr.Setup(x => x.ReservationRepository.AddAsync(It.IsAny<Reservation>()))
                .Callback<Reservation>(reserv => _reservations.Add(reserv));

            mgr.Setup(x => x.ReservationRepository.GetByIdAsync(reservation.Id,
                It.IsAny<Expression<Func<Reservation, bool>>>(), false))
                .ReturnsAsync(reservation);
            #endregion

            //#region verifies
            //mgr.Verify(x => x.PropertyRepository
            //.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Expression<Func<Property, bool>>>(),
            //true, "Host", "PropertyImages"), Times.Once());
            //mgr.Verify(x => x.ReservationRepository, Times.Exactly(5));
            //#endregion
            return mgr;
        }
        //public static Mock<IUnitOfWork> PrivacyTypeRepository()
        //{
        //    var privacyTypes = new Faker<PrivacyType>()
        //        .RuleFor(x => x.Id, Guid.NewGuid)
        //        .RuleFor(x => x.Name, x => x.Lorem.Letter(5))
        //        .RuleFor(x => x.CreatedAt, x => x.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
        //        .RuleFor(x => x.ModifiedAt, x => x.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
        //        .RuleFor(x => x.IsDisplayed, true).Generate(5);

        //    var mockRepo = new Mock<IUnitOfWork>();
        //    mockRepo.Setup(x => x.PrivacyTypeRepository.GetAllAsync(It.IsAny<Expression<Func<PrivacyType, bool>>>(), new PrivacyTypeParameters(),
        //        false))
        //        .ReturnsAsync(privacyTypes);

        //    mockRepo.Setup(x => x.PrivacyTypeRepository.AddAsync(It.IsAny<PrivacyType>())).Returns((PrivacyType privacyType) =>
        //    {
        //        privacyTypes.Add(privacyType);
        //        return privacyType;
        //    });
        //    return mockRepo;
        //}
    }
}
