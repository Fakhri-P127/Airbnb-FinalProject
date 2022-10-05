using Airbnb.Application.Common.Interfaces.Email;
using Airbnb.Application.Contracts.v1.Admin.EmailRelated.Responses;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Application.Features.Client.Reservations.Commands.Create;
using Airbnb.Application.Mapping;
using Airbnb.Application.Tests.Datas.ReservationDatas;
using Airbnb.Application.Tests.Mocks;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Moq;
using System.Security.Claims;
using Host = Airbnb.Domain.Entities.AppUserRelated.Host;

namespace Airbnb.Application.Tests.Features.ReservationsTests.Commands
{
    /// <summary>
    /// en yaxshi yazdigim unit test bu oldu, vaxt az oldugu uchun tekce bu handleri
    /// seliqeye sala bildim.
    /// 
    /// O biri handler lar yazdiqlarimla esasen eyni mentiqde oldugu uchun ya da bundan asan oldugu uchun
    /// ele bu qeder unit test yazdim. Birde property uchun yazmaq olardi amma o chox boyuk oldugu uchun 
    /// vaxtim chatmayacaq((
    /// </summary>

    public class CreateReservationCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IHttpContextAccessor> _mockAccessor;
        private readonly Mock<IEmailSender> _mockEmailSender;
        private  CreateReservationCommandHandler _handler;
        private readonly List<AppUser> _users;
        private readonly Host _host;
        private readonly List<Reservation> _reservations;
        private readonly Property _property;
        private readonly CreateReservationCommand _command;
        private readonly Guid _userId;

        public CreateReservationCommandHandlerTests()
        {
            _mockAccessor = new Mock<IHttpContextAccessor>();
            _mockEmailSender = new Mock<IEmailSender>();
            MapperConfiguration mapperConfig = new (config => config.AddProfile<ReservationMappings>());
            _mapper = mapperConfig.CreateMapper();

            #region datas needed for the unit test
            _users = DatasForReservationTests.CreateListOfUsers();
            _host = DatasForReservationTests.CreateHost(_users);
            _property = DatasForReservationTests.CreateProperty(_host);
            _reservations = DatasForReservationTests.CreateListOfReservations(_host, _users);

            _users.First().Host = _host;//birinci useri host edirik
            _host.Reservations.Add(_reservations.First());

            _command = DatasForReservationTests.CreateReservationCommand(_property);
            #endregion

            #region accessor arrange
            DefaultHttpContext context = new();
             _userId = _users.Last().Id;
            ClaimsIdentity claimIdentity = new(new[]
            {
                new Claim(ClaimTypes.NameIdentifier,_userId.ToString())
            });
            context.User = new ClaimsPrincipal(claimIdentity);
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);
            #endregion

            _handler = _handler = new(
                MockHelpers.MockedUnitOfWork(_reservations, _command, _property, _mapper).Object,
                _mapper, _mockAccessor.Object, _mockEmailSender.Object, MockHelpers.MockUserManager(_users).Object
                );
        }

        [Fact]
        public async Task Handle_WhenEverythingIsCorrect_CreatesNewReservation()
        {
            #region mock EmailSender 
            // bunu yazmagimin choxda menasi yoxdu, ozu default value ile edir ve o mene problem
            // yaratmadigi uchun bele de saxladim
            //_mockEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<MessageResponse>()));
            //.Callback<MessageResponse>(x=>x);
            #endregion

            var result = await _handler.Handle(_command, It.IsAny<CancellationToken>());

            result.Should().NotBeNull().And.BeOfType<PostReservationResponse>();
            _reservations.Count().Should().Be(3);//normalda 2 di

            // mock u static class a apardiqdan sonra verifylar ishlemeyecek ona gore commente atdim
            #region verifies
            _mockAccessor.Verify(x => x.HttpContext, Times.Once());
            //_mockUnit.Verify(x => x.PropertyRepository
            //.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Expression<Func<Property, bool>>>(),
            //true, "Host", "PropertyImages"), Times.Once());
            //_mockUnit.Verify(x => x.ReservationRepository, Times.Exactly(5));
            _mockEmailSender.Verify(x => x.SendEmailAsync(It.IsAny<MessageResponse>()), Times.Once());
            #endregion
        }
        [Fact]
        public async Task CheckExceptionThenReturnProperty_WhenReservingYourOwnProperty_ThrowReservation_CantReserveYourOwnPropertyException()
        {
            #region accessor arrange
            DefaultHttpContext context = new();
            Guid userId = _users.First().Id;//hostun appuser Id sine beraber edirik ki o exception i ala bilek
            ClaimsIdentity claimIdentity = new(new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userId.ToString())
            });
            context.User = new ClaimsPrincipal(claimIdentity);
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);
            #endregion

            Func<Task> act = async ()=> await _handler.Handle(_command, It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<Reservation_CantReserveYourOwnPropertyException>();

            #region verifies
            _mockAccessor.Verify(x => x.HttpContext, Times.Once());
            #endregion
        }

        [Fact]
        public async Task CheckMaxGuestThenReturnInt_WhenPropertyMaxGuestLimitIsLessThanGivenGuestCount_ThrowReservationMaxGuestCountValidationException()
        {
            _command.AdultCount = 5;
            _handler = new(MockHelpers.MockedUnitOfWork(_reservations, _command, _property, _mapper).Object,
                      _mapper, _mockAccessor.Object, _mockEmailSender.Object, MockHelpers.MockUserManager(_users).Object);

            Func<Task> act = async () => await _handler.Handle(_command, It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<ReservationMaxGuestCountValidationException>();

            #region verifies
            _mockAccessor.Verify(x => x.HttpContext, Times.Once());
            #endregion
        }

        [Fact]
        public async Task CheckOutDateValidationChecker_WhenPropertyMinNightLimitIsBiggerThanReservedDays_ThrowReservationMinNightValidationException()
        {
            // createReservation 2022-05-09 2022-05-11 e kimidir, 2 gun
            _property.MinNightCount = 4; // exception i yoxlamaq uchun
            _handler = new(MockHelpers.MockedUnitOfWork(_reservations, _command, _property, _mapper).Object,
                   _mapper, _mockAccessor.Object, _mockEmailSender.Object, MockHelpers.MockUserManager(_users).Object);

            Func<Task> act = async () => await _handler.Handle(_command, It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<ReservationMinNightValidationException>();

            #region verifies
            _mockAccessor.Verify(x => x.HttpContext, Times.Once());
            #endregion
        }
        [Fact]
        public async Task CheckOutDateValidationChecker_WhenPropertyMaxNightLimitIsLessThanReservedDays_ThrowReservationMaxNightValidationException()
        {
            _property.MaxNightCount = 4; // exception i yoxlamaq uchun
            _command.CheckInDate = new DateTime(2022, 05, 05);
            _command.CheckOutDate = new DateTime(2022, 05, 10);

            _handler = new(MockHelpers.MockedUnitOfWork(_reservations, _command, _property, _mapper).Object,
                     _mapper, _mockAccessor.Object, _mockEmailSender.Object, MockHelpers.MockUserManager(_users).Object);

            Func<Task> act = async () => await _handler.Handle(_command, It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<ReservationMaxNightValidationException>();
            
            #region verifies
            _mockAccessor.Verify(x => x.HttpContext, Times.Once());
            #endregion
        }
        [Fact]
        public async Task CheckIfDateOccupied_WhenCheckInDateIsOccupied_ThrowReservationCheckInOccupiedException()
        {
            _command.CheckInDate = new DateTime(2022, 05, 04);// exception verecek chunki 4u tutulub
            _command.CheckOutDate = new DateTime(2022, 05, 10);
            _handler = new(MockHelpers.MockedUnitOfWork(_reservations, _command, _property, _mapper).Object,
                   _mapper, _mockAccessor.Object, _mockEmailSender.Object, MockHelpers.MockUserManager(_users).Object);

            Func<Task> act = async () => await _handler.Handle(_command, It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<ReservationCheckInOccupiedException>();

            #region verifies
            _mockAccessor.Verify(x => x.HttpContext, Times.Once());
            #endregion
        }
        [Fact]
        public async Task CheckIfDateOccupied_WhenCheckOutDateIsOccupied_ThrowReservationCheckOutOccupiedException()
        {
            _command.CheckInDate = new DateTime(2022, 05, 01);
            _command.CheckOutDate = new DateTime(2022, 05, 05);
            _handler = new(MockHelpers.MockedUnitOfWork(_reservations, _command, _property, _mapper).Object,
                _mapper, _mockAccessor.Object, _mockEmailSender.Object, MockHelpers.MockUserManager(_users).Object);

            Func<Task> act = async () => await _handler.Handle(_command, It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<ReservationCheckOutOccupiedException>();

            #region verifies
            _mockAccessor.Verify(x => x.HttpContext, Times.Once());
            #endregion
        }

        [Fact]
        public async Task CheckIfDateOccupied_WhenDateContainsOccupiedDate_ThrowReservationContainsOccupiedDateException()
        {
            // bu date ler 2022-05-03 -- 2022-05-06 tarixli reservation i ichinde barindirir
            // ve bu occupiedDateException verecek 
            _command.CheckInDate = new DateTime(2022, 05, 01);
            _command.CheckOutDate = new DateTime(2022, 05, 10);
            _handler = new(MockHelpers.MockedUnitOfWork(_reservations, _command, _property, _mapper).Object,
                _mapper, _mockAccessor.Object, _mockEmailSender.Object, MockHelpers.MockUserManager(_users).Object);

            Func<Task> act = async () => await _handler.Handle(_command, It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<ReservationContainsOccupiedDateException>();

            #region verifies
            _mockAccessor.Verify(x => x.HttpContext, Times.Once());
            #endregion
        }

        [Fact]
        public async Task ManuallySettingValuesToReservation_WhenReservationHasPetCountAndPropertyDoesNotAllowPets_ThrowReservation_PetsAreNotAllowedException()
        {
            _command.PetCount = 1;
            _property.IsPetAllowed = false;
            _handler = new(MockHelpers.MockedUnitOfWork(_reservations, _command, _property, _mapper).Object,
                _mapper, _mockAccessor.Object, _mockEmailSender.Object, MockHelpers.MockUserManager(_users).Object);

            Func<Task> act = async () => await _handler.Handle(_command, It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<Reservation_PetsAreNotAllowedException>();

            #region verifies
            _mockAccessor.Verify(x => x.HttpContext, Times.Once());
            #endregion
        }
    }
}
