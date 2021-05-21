using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AutoMapper;
using BLL.App;
using Contracts.BLL.App;
using DAL.App.EF;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Identity;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.IntegrationTests
{
    public class TestControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<WebApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;
        private static JwtResponse? _jwt;
        private JwtResponse? _register;
        private DbContextOptionsBuilder<AppDbContext> optionBuilder;


        public TestControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // provide new random database name here
            // or parallel test methods will conflict each other
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("test_database_name", Guid.NewGuid().ToString());
                })
                .CreateClient(new WebApplicationFactoryClientOptions()
                    {
                        // dont follow redirects
                        AllowAutoRedirect = false
                    }
                );
        }


        [Fact]
        public async Task TestAction_HasSuccessStatusCode()
        {
            // ARRANGE
            var uri = "/test/test";

            // ACT
            var getTestResponse = await _client.GetAsync(uri);

            // ASSERT
            getTestResponse.EnsureSuccessStatusCode();
            Assert.InRange((int)getTestResponse.StatusCode, 200, 299);
        }

        [Fact]
        public async Task TestAuthAction_AuthRedirect()
        {
            // ARRANGE
            var uri = "/test/TestAuth";

            // ACT
            var getTestResponse = await _client.GetAsync(uri);

            // ASSERT
            Assert.Equal(302, (int) getTestResponse.StatusCode);
        }

        [Fact]
        public async Task Get_Register()
        {

            var uri = "/api/v1/Account/Register";

            var register = new Register
            {
                Email = "test@user.ee",
                Password = "Foo.bar1",
                Firstname = "Test",
                Lastname = "user"
            };

            string stringData = JsonConvert.SerializeObject(register);
            stringData.Should().NotBeNullOrEmpty();
            var contentData = new StringContent(stringData,
                System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync
                (uri, contentData);


            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var data = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(body);

            Assert.Equal("Test", data!.Firstname);
            Assert.Equal("user", data.Lastname);

            _register = data;
            await TestLogin();


        }
   
        private async Task TestLogin()
        {
            var uri = "/api/v1/Account/Login";
            var jwt = "";

            var contentType = new MediaTypeWithQualityHeaderValue
                ("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);


            var login = new Login
            {
                Email = "test@user.ee",
                Password = "Foo.bar1",

            };
            string stringData = JsonConvert.SerializeObject(login);
            var contentData = new StringContent(stringData,
                System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync
                (uri,contentData).Result;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<JwtResponse>(content);
                jwt = resp.Token;

            }
            Assert.NotEmpty(jwt);


            //Add product
            uri = "/api/v1/Products";
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer",
                    jwt);

            await PostCity();
            await PostCategory();
            await PostCondition();
            await PostCounty();
            await PostUnits();
            await PostProduct();


            var getResponse = await _client.GetAsync(uri);
            getResponse.EnsureSuccessStatusCode();
            var getContent = await getResponse.Content.ReadAsStringAsync();
            var getRes = JsonConvert.DeserializeObject<Product[]>(getContent);

            var product = getRes[0];
            var productId = product.Id;

            Assert.True("Katkine kapp" == product.Description);

            uri = "/api/v1/Products/" + productId;

            await EditProduct(uri);
            await DeleteProduct(uri);
        }

        private async Task DeleteProduct(string uri)
        {
            var getProduct = await _client.GetAsync(uri);
            getProduct.EnsureSuccessStatusCode();
            var productBody = await getProduct.Content.ReadAsStringAsync();
            var productData = JsonHelper.DeserializeWithWebDefaults<Product>(productBody);

            var serializedBody = JsonConvert.SerializeObject(productData);
            serializedBody.Should().NotBeNullOrEmpty();

            var response = await _client.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();

            uri = "/api/v1/Products";
            var getAllResponse = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var getAllContent = await getAllResponse.Content.ReadAsStringAsync();
            var getAllResp = JsonConvert.DeserializeObject<List<Product>>(getAllContent)!;

            Assert.True(0 == getAllResp.Count);

        }

        private async Task EditProduct(string uri)
        {
            var getProduct = await _client.GetAsync(uri);
            getProduct.EnsureSuccessStatusCode();
            var productBody = await getProduct.Content.ReadAsStringAsync();
            var productData = JsonHelper.DeserializeWithWebDefaults<Product>(productBody);

            productData!.Description = "Uus nimi";
            var serializedBody = JsonConvert.SerializeObject(productData);
            serializedBody.Should().NotBeNullOrEmpty();
            var httpContent = new StringContent(serializedBody!, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(uri, httpContent);
            response.EnsureSuccessStatusCode();

            getProduct = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var body = await getProduct.Content.ReadAsStringAsync();
            var data = JsonHelper.DeserializeWithWebDefaults<PublicApi.DTO.v1.Product>(body);
            Assert.Equal("Uus nimi", data!.Description);
        }

        private async Task PostProduct()
        {

            var uri = "/api/v1/Products";

            var getUser = await _client.GetAsync("/api/v1/AppUser");
            getUser.EnsureSuccessStatusCode();
            var userBody = await getUser.Content.ReadAsStringAsync();
            var userData = JsonHelper.DeserializeWithWebDefaults<List<AppUser>>(userBody);

            var getCity = await _client.GetAsync("/api/v1/Cities");
            getCity.EnsureSuccessStatusCode();
            var cityBody = await getCity.Content.ReadAsStringAsync();
            var cityData = JsonHelper.DeserializeWithWebDefaults<List<PublicApi.DTO.v1.City>>(cityBody);

            var getCounty = await _client.GetAsync("/api/v1/Counties");
            getCounty.EnsureSuccessStatusCode();
            var countyBody = await getCounty.Content.ReadAsStringAsync();
            var countyData = JsonHelper.DeserializeWithWebDefaults<List<PublicApi.DTO.v1.County>>(countyBody);

            var getCondition = await _client.GetAsync("/api/v1/Conditions");
            getCondition.EnsureSuccessStatusCode();
            var conditionBody = await getCondition.Content.ReadAsStringAsync();
            var conditionData = JsonHelper.DeserializeWithWebDefaults<List<PublicApi.DTO.v1.Condition>>(conditionBody);

            var getCategory = await _client.GetAsync("/api/v1/Categories");
            getCategory.EnsureSuccessStatusCode();
            var categoryBody = await getCategory.Content.ReadAsStringAsync();
            var categoryData = JsonHelper.DeserializeWithWebDefaults<List<PublicApi.DTO.v1.Category>>(categoryBody);

            var getUnit = await _client.GetAsync("/api/v1/Units");
            getUnit.EnsureSuccessStatusCode();
            var unitBody = await getUnit.Content.ReadAsStringAsync();
            var unitData = JsonHelper.DeserializeWithWebDefaults<List<PublicApi.DTO.v1.Unit>>(unitBody);

            var product =  new Product
            {
                Description = "Katkine kapp",
                Color = "kollane",
                ProductAge = 10,
                IsBooked = false,
                HasTransport = false,
                Height = 40,
                Width = 40,
                AppUserId = userData![1].Id,
                DateAdded = DateTime.Now,
                Depth = 40,
                LocationDescription = "Vanalinn",
                CityId = cityData![0].Id,
                CountyId = countyData![0].Id,
                CategoryId = categoryData![0].Id,
                UnitId = unitData![0].Id,
                ConditionId = conditionData![0].Id,

            };
            var serializedBody = JsonConvert.SerializeObject(product);
            serializedBody.Should().NotBeNullOrEmpty();
            var httpContent = new StringContent(serializedBody!, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, httpContent);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var data = JsonHelper.DeserializeWithWebDefaults<PublicApi.DTO.v1.Product>(body);
            Assert.Equal("Katkine kapp", data!.Description);

        }



        private async Task PostCity()
        {
            var uri = "/api/v1/Cities";

             var city =  new City
            {
                Name = "Tallinn",

            };
             var serializedBody = JsonConvert.SerializeObject(city);
             serializedBody.Should().NotBeNullOrEmpty();
             var httpContent = new StringContent(serializedBody!, Encoding.UTF8, "application/json");
             var response = await _client.PostAsync(uri, httpContent);
             response.EnsureSuccessStatusCode();

             var body = await response.Content.ReadAsStringAsync();
             var data = JsonHelper.DeserializeWithWebDefaults<PublicApi.DTO.v1.City>(body);
             Assert.Equal("Tallinn", data!.Name);
        }
        private async Task PostCounty()
        {
            var uri = "/api/v1/Counties";

            var county =  new County
            {
                Name = "Harjumaa",

            };
            var serializedBody = JsonConvert.SerializeObject(county);
            serializedBody.Should().NotBeNullOrEmpty();
            var httpContent = new StringContent(serializedBody!, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, httpContent);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var data = JsonHelper.DeserializeWithWebDefaults<PublicApi.DTO.v1.County>(body);
            Assert.Equal("Harjumaa", data!.Name);
        }
        private async Task PostCondition()
        {
            var uri = "/api/v1/Conditions";

            var condition =  new Condition
            {
                Description = "Vana",

            };
            var serializedBody = JsonConvert.SerializeObject(condition);
            serializedBody.Should().NotBeNullOrEmpty();
            var httpContent = new StringContent(serializedBody!, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, httpContent);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var data = JsonHelper.DeserializeWithWebDefaults<PublicApi.DTO.v1.Condition>(body);
            Assert.Equal("Vana", data!.Description);
        }

        private async Task PostCategory()
        {
            var uri = "/api/v1/Categories";

            var category =  new Category
            {
                Name = "Toolid",

            };
            var serializedBody = JsonConvert.SerializeObject(category);
            serializedBody.Should().NotBeNullOrEmpty();
            var httpContent = new StringContent(serializedBody!, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, httpContent);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var data = JsonHelper.DeserializeWithWebDefaults<PublicApi.DTO.v1.Category>(body);
            Assert.Equal("Toolid", data!.Name);
        }
        private async Task PostUnits()
        {
            var uri = "/api/v1/Units";

            var unit =  new Unit
            {
                Name = "cm",

            };
            var serializedBody = JsonConvert.SerializeObject(unit);
            serializedBody.Should().NotBeNullOrEmpty();
            var httpContent = new StringContent(serializedBody!, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, httpContent);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var data = JsonHelper.DeserializeWithWebDefaults<PublicApi.DTO.v1.Unit>(body);
            Assert.Equal("cm", data!.Name);
        }
    }

}
