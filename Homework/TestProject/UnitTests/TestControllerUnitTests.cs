using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App;
using Domain.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Controllers;
using WebApp.ViewModels.Test;
using Xunit;
using Xunit.Abstractions;
using City = PublicApi.DTO.v1.City;

namespace TestProject.UnitTests
{
    public class TestControllerUnitTests
    {
        private readonly TestController _testController;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;
        private DbContextOptionsBuilder<AppDbContext> optionBuilder;


        // ARRANGE - common
        public TestControllerUnitTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            // set up db context for testing - using InMemory db engine
            optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // provide new random database name here
            // or parallel test methods will conflict each other
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<TestController>();

            // SUT
            _testController = new TestController(logger, _ctx);
        }

        public IAppBLL GetBLL()
        {
            var context = new AppDbContext(optionBuilder.Options);


            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DAL.App.DTO.MappingProfiles.AutoMapperProfile>();
                cfg.AddProfile<BLL.App.DTO.MappingProfiles.AutoMapperProfile>();
            });
            var mapper = mockMapper.CreateMapper();
            var uow = new AppUnitOfWork(context, mapper);
            return new AppBLL(uow, mapper);
        }



        [Fact]
        public async Task Action_Test__Returns_ViewModel()
        {
            //see test tagastab vaid viewmodeli ja count peaks olema alati 0
            // ACT
            var result = await _testController.Test();

            // ASSERT
            Assert.NotNull(result); // poleks null
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult); //viewmodel poleks null
            var vm = viewResult!.Model;
            Assert.IsType<TestViewModel>(vm);
            var testVm = vm as TestViewModel;
            Assert.NotNull(testVm!.Products);
            // for debugging
            Assert.Equal(0, testVm.Products.Count);
        }

        [Fact]
        public async Task Action_Test__Returns_ViewModel_WithData()
        {
            // ARRANGE
            await SeedData();

            // ACT
            var result = await _testController.Test();

            // ASSERT
            var testVm = (result as ViewResult)?.Model as TestViewModel;
            Assert.NotNull(testVm);
            // _testOutputHelper.WriteLine($"Count of elements: {testVm.ContactTypes.Count}");
            Assert.Equal(2, testVm!.Products.Count);
            Assert.Equal("Proov kapp 0", testVm.Products.First()!.Description);
        }

        [Fact]
        public async Task Action_Test__Returns_ViewModel_WithNoData__Fails_With_Exception()
        {

            // ACT
            var result = await _testController.Test();

            // ASSERT
            var testVm = (result as ViewResult)?.Model as TestViewModel;
            Assert.NotNull(testVm);
            // _testOutputHelper.WriteLine($"Count of elements: {testVm.ContactTypes.Count}");

            Assert.ThrowsAny<Exception>(() => testVm!.Products.First());
        }


        [Theory]
        //[InlineData(5)]
        [ClassData(typeof(CountGenerator))]
        public async Task Action_Test__Returns_ViewModel_WithData_Fluent(int count)
        {
            // ARRANGE
            await SeedData(count);

            // ACT
            var result = await _testController.Test();

            // ASSERT
            var testVm = (result as ViewResult)?.Model as TestViewModel;
            testVm.Should().NotBeNull();
            testVm!.Products
                .Should().NotBeNull()
                .And.HaveCount(count)
                .And.Contain(ct => ct.Description!.ToString() == "Proov kapp 0")
                .And.Contain(ct => ct.Description!.ToString() == $"Proov kapp {count - 1}");
        }

        [Fact]
        public async Task Action_Test__Returns_Model_WithData()
        {
            var _bll = GetBLL();
            // ARRANGE
            await SeedDataBLL(_bll);

            // ACT
            var result = await _bll.Product.GetAllAsync();

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal("Vedruvoodi", result.Select(p => p.Description).First());
        }


        [Fact]
        public async Task Action_Test__Returns_Model_WithOneEntity()
        {
            var _bll = GetBLL();
            // ARRANGE
            await SeedDataBLL(_bll);
            // ACT

            var all = await _bll.Product.GetAllProductsAsync();
            var products = all.ToList();
            var result = await _bll.Product.FirstOrDefaultDTOAsync(products[0].Id);


            Assert.NotNull(result);
            Assert.Equal("Kapp kahe jalaga", result!.Description);
        }



        [Fact]
        public async Task Action_Test__Returns_Model_WithEditedData()
        {
            var _bll = GetBLL();
            // ARRANGE
            await SeedDataBLL(_bll);
            // ACT

            var all = await _bll.Product.GetAllProductsAsync();
            var products = all.ToList();
            var result = await _bll.Product.FirstOrDefaultAsync(products[0].Id);
            await EditData(result, _bll);

            Assert.NotNull(result);
            Assert.Equal("Kapp kolme jalaga", result!.Description);
        }

        private async Task EditData(BLL.App.DTO.Product product, IAppBLL _bll)
        {
            _bll = GetBLL();
            product.Description = "Kapp kolme jalaga";

            _bll.Product.Update(product);
            await _bll.SaveChangesAsync();
        }

        [Fact]
        public async Task Action_Test__RemovesEntity()
        {
            var _bll = GetBLL();
            // ARRANGE
            await SeedDataBLL(_bll);
            _bll = GetBLL();
            var all = await _bll.Product.GetAllProductsAsync();
            var products = all.ToList();

            await _bll.Product.RemoveAsync(products[0].Id);

            await _bll.SaveChangesAsync();
            // ACT
            var result = await _bll.Product.GetAllAsync();

            // ASSERT
            Assert.Single(result);
        }

        [Fact]
        public async Task Action_Test__DeleteProduct()
        {
            var _bll = GetBLL();
            // ARRANGE
            await SeedDataBLL(_bll);
            _bll = GetBLL();
            var all = await _bll.Product.GetAllProductsAsync();
            var products = all.ToList();

            _bll.Product.RemoveProductAsync(products[0].Id);

            await _bll.SaveChangesAsync();
            // ACT
            var result = await _bll.Product.GetAllAsync();

            // ASSERT
            Assert.Single(result);
        }

        [Fact]
        public async Task Action_Test__DeleteAllUserProducts()
        {
            var _bll = GetBLL();
            // ARRANGE
            await SeedDataBLL(_bll);
            _bll = GetBLL();

            var all = await _bll.Product.GetAllProductsAsync();
            var products = all.ToList();
            var appUser = products[0].AppUserId;
            _bll.Product.DeleteAll(appUser);

            await _bll.SaveChangesAsync();
            // ACT
            var result = await _bll.Product.GetAllAsync();

            // ASSERT
            Assert.Single(result);
        }


    [Fact]
        public async Task Action_Test__WithChangeBookingStatus()
        {
            var _bll = GetBLL();
            // ARRANGE
            await SeedDataBLL(_bll);
            // ACT

            var all = await _bll.Product.GetAllProductsAsync();
            var products = all.ToList();
            var result = await _bll.Product.FirstOrDefaultAsync(products[0].Id);
            await ChangeBookingStatus(result, _bll);

            Assert.NotNull(result);
            Assert.True(result!.IsBooked);
        }



        private async Task ChangeBookingStatus(BLL.App.DTO.Product product, IAppBLL _bll)
        {
            _bll = GetBLL();
            product.IsBooked = true;

            _bll.Product.Update(product);
            await _bll.SaveChangesAsync();
        }
        [Fact]
        public async Task Action_Test__GetAllProductsIsNotBookedAsync()
        {
            var _bll = GetBLL();
            // ARRANGE
            await SeedDataBLL(_bll);

            // ACT
            var result = await _bll.Product.GetAllProductsIsNotBookedAsync();

            // ASSERT
            Assert.NotNull(result);

            Assert.Single(result);

        }
        [Fact]
        public async Task Action_Test__GetLastInserted()
        {
            var _bll = GetBLL();
            // ARRANGE
            await SeedDataBLL(_bll);

            // ACT
            var result = await _bll.Product.GetLastInserted();

            // ASSERT
            Assert.NotNull(result);

            Assert.True(result.Count() == 2);

        }
        [Fact]
        public async Task Action_Test__GetSearchResult()
        {
            var _bll = GetBLL();
            // ARRANGE
            await SeedDataBLL(_bll);


            var cityList = await _bll.City.GetAllAsync();
            var cities = cityList.ToList();
            var countyList = await _bll.County.GetAllAsync();
            var counties = countyList.ToList();
            var categoryList = await _bll.Category.GetAllAsync();
            var categories = categoryList.ToList();
            var result = await _bll.Product.GetSearchResult(counties[0].Id, cities[0].Id,
                categories[0].Id);

            // ASSERT
            Assert.NotNull(result);

            Assert.True(result.Count() == 1);

        }

        [Fact]
        public async Task Action_Test__GetId()
        {
            var _bll = GetBLL();
            // ARRANGE
            await SeedDataBLL(_bll);
            // ACT

            var all = await _bll.Product.GetAllProductsAsync();
            var products = all.ToList();
            var appUser = products[0].AppUserId;
            var result = await _bll.Product.GetId(appUser);

            Assert.NotNull(result);
            Assert.True(result.Count() == 1);
        }



        private async Task SeedDataBLL(IAppBLL tempbll)
        {

            await tempbll.SaveChangesAsync();

            tempbll.City.Add(new BLL.App.DTO.City
            {
                Name = "Tartu"
            });
            tempbll.City.Add(new BLL.App.DTO.City
            {
                Name = "Tallinn"
            });
            tempbll.County.Add(new BLL.App.DTO.County
            {
                Name = "Tartumaa"
            });
            tempbll.County.Add(new BLL.App.DTO.County
            {
                Name = "Harjumaa"
            });
            tempbll.Condition.Add(new BLL.App.DTO.Condition
            {
                Description = "Vana"
            });
            tempbll.Condition.Add(new BLL.App.DTO.Condition
            {
                Description = "Uus"
            });
            tempbll.Category.Add(new BLL.App.DTO.Category()
            {
                Name = "Kapid"
            });
            tempbll.Category.Add(new BLL.App.DTO.Category()
            {
                Name = "Voodid"
            });
            tempbll.Unit.Add(new BLL.App.DTO.Unit()
            {
                Name = "cm"
            });
            tempbll.Unit.Add(new BLL.App.DTO.Unit()
            {
                Name = "dm"
            });
            await tempbll.SaveChangesAsync();
            var _bll = GetBLL();
            var county = await _bll.County.GetAllAsync();
            var countyObject = county.ToList();

            var city = await _bll.City.GetAllAsync();
            var cityObject = city.ToList();

            var condition = await _bll.Condition.GetAllAsync();
            var conditionObject = condition.ToList();

            var category = await _bll.Category.GetAllAsync();
            var categoryObject = category.ToList();

            var unit = await _bll.Unit.GetAllAsync();
            var unitObject = unit.ToList();

            tempbll.Product.Add(new BLL.App.DTO.Product
            {
                Description = "Kapp kahe jalaga",
                Color = "punane",
                ProductAge = 2,
                Width = 20,
                Height = 20,
                Depth = 20,
                LocationDescription = "vanalinn",
                IsBooked = true,
                HasTransport = false,
                AppUserId = new Guid("6139f21a-7af9-4a46-a071-e643c4f492d1"),
                CountyId = countyObject[0].Id,
                CityId = cityObject[0].Id,
                ConditionId = conditionObject[0].Id,
                CategoryId = categoryObject[0].Id,
                UnitId = unitObject[0].Id,
                DateAdded = DateTime.Now

            });
            tempbll.Product.Add(new BLL.App.DTO.Product
            {
                Description = "Vedruvoodi",
                Color = "sinine",
                ProductAge = 10,
                Width = 40,
                Height = 30,
                Depth = 50,
                LocationDescription = "kristiine",
                IsBooked = false,
                HasTransport = false,
                AppUserId = new Guid("6139f21a-7af9-4a46-a071-e643c4f492d2"),
                CountyId = countyObject[1].Id,
                CityId = cityObject[1].Id,
                ConditionId = conditionObject[1].Id,
                CategoryId = categoryObject[1].Id,
                UnitId = unitObject[1].Id,
                DateAdded = DateTime.Now

            });

            await tempbll.SaveChangesAsync();
        }


        private async Task SeedData(int count = 2)
        {
            for (int i = 0; i < count; i++)
            {
                _ctx.Products.Add(new Product()
                {
                    Description = $"Proov kapp {i}",
                    Color = "punane",
                    Width = 5,
                    Height = 10,
                    Depth = 13,
                    Condition = new Condition
                    {
                        Description = new LangString("Vana")
                    },
                    County = new County
                    {
                        Name = new LangString("Valgamaa")
                    },
                    Category = new Category
                    {
                        Name = new LangString("Kapid")
                    },
                    LocationDescription = "Vanalinn"

                });
            }
            await _ctx.SaveChangesAsync();
        }
    }






    public class CountGenerator : IEnumerable<object[]>
    {
        private static List<object[]> _data
        {
            get
            {
                var res = new List<Object[]>();
                for (int i = 1; i <= 100; i++)
                {
                    res.Add(new object[]{i});
                }

                return res;
            }
        }

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
