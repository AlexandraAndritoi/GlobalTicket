using AutoMapper;
using GlobalTicket.TicketManagement.Application.Contracts.Persistance;
using GlobalTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using GlobalTicket.TicketManagement.Application.Profiles;
using GlobalTicket.TicketManagement.Application.Tests.Mocks;
using GlobalTicket.TicketManagement.Domain.Entities;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GlobalTicket.TicketManagement.Application.Tests.Categories.Queries
{
    public class GetCategoriesListQueryHandlerTests
    {
        private readonly Mock<IAsyncRepository<Category>> mockCategoryRepository;
        private readonly IMapper mapper;

        public GetCategoriesListQueryHandlerTests()
        {
            mockCategoryRepository = RepositoryMock.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCategoriesListTest()
        {
            var handler = new GetCategoriesListQueryHandler(mockCategoryRepository.Object, mapper);

            var result = await handler.Handle(new GetCategoriesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CategoryListViewModel>>();

            result.Count.ShouldBe(4);
        }
    }
}
