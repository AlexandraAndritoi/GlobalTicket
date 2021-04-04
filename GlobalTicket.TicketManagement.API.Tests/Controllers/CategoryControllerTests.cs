using GlobalTicket.TicketManagement.API.Tests.Base;
using GlobalTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GlobalTicket.TicketManagement.API.Tests.Controllers
{
    public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CategoryControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessResult()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/category/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<CategoryListViewModel>>(responseString);

            Assert.IsType<List<CategoryListViewModel>>(result);
            Assert.NotEmpty(result);
        }
    }
}
