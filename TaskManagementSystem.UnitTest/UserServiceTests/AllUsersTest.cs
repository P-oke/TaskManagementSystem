using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Models;

namespace TaskManagementSystem.UnitTests.UserTests
{
    public class AllUsersTest
    {
        private readonly UserServiceFactory _fac;

        public AllUsersTest()
        {
            _fac = new UserServiceFactory();
        }

        [Fact]
        public async Task AllUsersAndTasks_ShouldWork()
        {
            //Arrange


            //Act

            var user = TestData.User();
            await _fac.Context.Users.AddAsync(user);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.UserService.AllUsers();

            Assert.NotNull(result);
            Assert.True(result.Data.Count >= 1);

        }


        [Fact]
        public async Task AllUsersAndTasks_ShouldRetirn_PaginatedData() 
        {
            //Arrange


            //Act

            var user = TestData.User();
            await _fac.Context.Users.AddAsync(user);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.UserService.AllUsers(new BaseSearchViewModel { Keyword = "TMS"});

            Assert.NotNull(result);
            Assert.True(result.Data.Count >= 1);

        }

    }   
}
