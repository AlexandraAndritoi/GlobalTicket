using AutoMapper;
using GlobalTicket.TicketManagement.Application.Contracts.Persistance;
using GlobalTicket.TicketManagement.Application.Features.Categories.Commands.CreateCategory;
using GlobalTicket.TicketManagement.Application.Profiles;
using GlobalTicket.TicketManagement.Application.Tests.Mocks;
using GlobalTicket.TicketManagement.Domain.Entities;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GlobalTicket.TicketManagement.Application.Tests.Categories.Commands
{
    public class CreateCategoryTests
    {
        private readonly Mock<IAsyncRepository<Category>> mockCategoryRepository;
        private readonly IMapper mapper;

        public CreateCategoryTests()
        {
            mockCategoryRepository = RepositoryMock.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoriesRepo()
        {
            var handler = new CreateCategoryCommandHandler(mockCategoryRepository.Object, mapper);

            await handler.Handle(new CreateCategoryCommand() { Name = "Test" }, CancellationToken.None);

            var allCategories = await mockCategoryRepository.Object.ListAllAsync();
            allCategories.Count.ShouldBe(5);
        }
    }
}
