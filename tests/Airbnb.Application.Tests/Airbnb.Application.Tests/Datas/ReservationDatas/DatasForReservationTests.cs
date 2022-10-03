using Airbnb.Application.Features.Client.Reservations.Commands.Create;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using Bogus;
using static Airbnb.Application.Contracts.v1.ApiRoutes;

namespace Airbnb.Application.Tests.Datas.ReservationDatas
{
    public static class DatasForReservationTests
    {
        public static List<AppUser> CreateListOfUsers()
        {
            List<AppUser> _users = new Faker<AppUser>("az")
            .RuleFor(x => x.Id, d => d.Random.Guid())
            .RuleFor(x => x.Firstname, d => d.Person.FirstName)
            .RuleFor(x => x.Lastname, d => d.Person.LastName)
            .RuleFor(x => x.UserName, d => d.Person.UserName)
            .RuleFor(x => x.Email, d => d.Person.Email)
            .RuleFor(x => x.DateOfBirth, d => d.Date.Between(DateTime.Now.AddYears(-100),
            DateTime.Now.AddYears(-18)))
            .RuleFor(x => x.PhoneNumber, d => d.Person.Phone)
            .Generate(5);

            return _users;
        }
        public static Property CreateProperty(Host _host)
        {
            Property property = new Property()
            {
                Id = Guid.NewGuid(),
                Title = "salamlar",
                CheckInTime = new TimeSpan(15, 00, 00),
                CheckOutTime = new TimeSpan(15, 00, 00),
                Price = 444,
                HostId = _host.Id,
                IsPetAllowed = true,
                MaxGuestCount = 4,
                MinNightCount = 1,//exception yoxlanilir
                Reservations = new(),
                Host = _host
            };
            return property;
        }
        public static Host CreateHost(List<AppUser> _users)
        {
            Host host = new Faker<Host>()
          .RuleFor(x => x.Id, d => d.Random.Guid())
             .RuleFor(x => x.Status, d => d.Random.Number(1, 3))
             .RuleFor(x => x.AppUserId, _users.First().Id)
             .Generate();
            return host;
        }
        public static List<Reservation> CreateListOfReservations(
            Host _host,List<AppUser> _users)
        {
            List<Reservation > _reservations = new Faker<Reservation>()
            .RuleFor(x => x.Id, d => d.Random.Guid())
               .RuleFor(x => x.HostId, _host.Id)
               .RuleFor(x => x.AppUserId, _users.First().Id)
               .RuleFor(x => x.Status, d => d.Random.Number(1, 6))
               .RuleFor(x => x.AdultCount, d => d.Random.Number(1, 5))
               .RuleFor(x => x.ChildCount, d => d.Random.Number(1, 3))
               .RuleFor(x => x.CheckInDate, d => d.Date.Between(DateTime.Now, DateTime.Now.AddYears(1)))
               .RuleFor(x => x.CheckOutDate, d => d.Date.Between(DateTime.Now.AddDays(1), DateTime.Now.AddYears(1)))
               .Generate(2);

            #region dates that are occupied
            _reservations.First().CheckInDate = new DateTime(2022, 05, 03);
            _reservations.First().CheckOutDate = new DateTime(2022, 05, 06);
            _reservations.Last().CheckInDate = new DateTime(2022, 10, 01);
            _reservations.Last().CheckOutDate = new DateTime(2022, 10, 03);
            #endregion
            return _reservations;
        }
        public static CreateReservationCommand CreateReservationCommand(Property _property)
        {
            CreateReservationCommand command = new()
            {
                AdultCount = 1,
                CheckInDate = new DateTime(2022, 05, 09),
                CheckOutDate = new DateTime(2022, 05, 11),
                ChildCount = 0,
                PropertyId = _property.Id
            };
            return command;
        }
    }
}
