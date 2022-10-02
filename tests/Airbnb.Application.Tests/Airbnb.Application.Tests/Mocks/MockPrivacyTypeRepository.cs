//using Airbnb.Application.Common.Interfaces;
//using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Parameters;
//using Airbnb.Domain.Entities.PropertyRelated;
//using Bogus;
//using Moq;
//using System.Linq.Expressions;

//namespace Airbnb.Application.Tests.Mocks
//{
//    public static class MockPrivacyTypeRepository
//    {

//        public static Mock<IUnitOfWork> PrivacyTypeRepository()
//        {
//            var privacyTypes = new Faker<PrivacyType>()
//                .RuleFor(x => x.Id, Guid.NewGuid)
//                .RuleFor(x => x.Name, x => x.Lorem.Letter(5))
//                .RuleFor(x => x.CreatedAt, x => x.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
//                .RuleFor(x => x.ModifiedAt, x => x.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
//                .RuleFor(x => x.IsDisplayed, true).Generate(5);

//            var mockRepo = new Mock<IUnitOfWork>();
//            mockRepo.Setup(x => x.PrivacyTypeRepository.GetAllAsync(It.IsAny<Expression<Func<PrivacyType, bool>>>(), new PrivacyTypeParameters(),
//                false))
//                .ReturnsAsync(privacyTypes);

//            mockRepo.Setup(x => x.PrivacyTypeRepository.AddAsync(It.IsAny<PrivacyType>())).Returns((PrivacyType privacyType) =>
//            {
//                privacyTypes.Add(privacyType);
//                return privacyType;
//            });
//            return mockRepo;
//        }
//    }
//}
