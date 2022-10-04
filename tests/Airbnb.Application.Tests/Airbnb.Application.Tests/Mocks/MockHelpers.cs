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
            Mock<IUserStore<TUser>> mockUserStore = new ();
            Mock<CustomUserManager<TUser>> mockUserManager = new(mockUserStore.Object, null, null, null, null, null, 
                null, null, null);
            mockUserManager.Object.UserValidators.Add(new UserValidator<TUser>());
            mockUserManager.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => users.Add(x));
            mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success)
                .Callback<TUser>(user=>users.Remove(user));
            mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
               .ReturnsAsync(users.Last());//testleri sonuncu ile edirem deye ele bunu cagira bilerem

            return mockUserManager;
        }
        public static Mock<IUnitOfWork> MockedUnitOfWork(List<Reservation> _reservations,
            CreateReservationCommand command,Property _property,IMapper _mapper) 
        {
            Mock<IUnitOfWork> mockUnit = new();

            #region predicates
            Func<Reservation, bool> checkInPredicate = x => x.CheckInDate <= command.CheckInDate
                && x.CheckOutDate >= command.CheckInDate;
            Func<Reservation, bool> checkOutPredicate = x => x.CheckInDate <= command.CheckOutDate
                            && x.CheckOutDate >= command.CheckOutDate;
            Func<Reservation, bool> containsOccupiedDatePredicate = x =>
            x.CheckInDate >= command.CheckInDate && x.CheckInDate <= command.CheckOutDate;
            #endregion
            #region mock for propertyrepo
            mockUnit.Setup(x => x.PropertyRepository.GetByIdAsync(command.PropertyId,
                It.IsAny<Expression<Func<Property, bool>>>(), true, "Host", "PropertyImages"))
                .ReturnsAsync(_property);
            #endregion
            #region unit setups for reservations
            //checkin
            mockUnit.Setup(x => x.ReservationRepository
                .GetSingleAsync(x => x.CheckInDate <= command.CheckInDate
                && x.CheckOutDate >= command.CheckInDate, false))
                .ReturnsAsync(_reservations.FirstOrDefault(checkInPredicate));
            //checkout
            mockUnit.Setup(x => x.ReservationRepository
              .GetSingleAsync(x => x.CheckInDate <= command.CheckOutDate
                            && x.CheckOutDate >= command.CheckOutDate, false))
              .ReturnsAsync(_reservations.FirstOrDefault(checkOutPredicate));
            //contains occupied date
            mockUnit.Setup(x => x.ReservationRepository
            .GetAllAsync(x => x.CheckInDate >= command.CheckInDate && x.CheckInDate <= command.CheckOutDate,
            null, false))
                .ReturnsAsync(_reservations.Where(containsOccupiedDatePredicate).ToList());

            Reservation reservation = _mapper.Map<Reservation>(command);
            reservation.Property = _property;
            mockUnit.Setup(x => x.ReservationRepository.AddAsync(It.IsAny<Reservation>()))
                .Callback<Reservation>(reserv => _reservations.Add(reserv));

            mockUnit.Setup(x => x.ReservationRepository.GetByIdAsync(reservation.Id,
                It.IsAny<Expression<Func<Reservation, bool>>>(), false))
                .ReturnsAsync(reservation);
            #endregion

            //#region verifies
            //mgr.Verify(x => x.PropertyRepository
            //.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Expression<Func<Property, bool>>>(),
            //true, "Host", "PropertyImages"), Times.Once());
            //mgr.Verify(x => x.ReservationRepository, Times.Exactly(5));
            //#endregion
            return mockUnit;
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
