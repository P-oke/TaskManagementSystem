using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Models;

namespace TaskManagementSystem.UnitTests.UserTests
{
    public class AllUsersAndTasksTest
    {
        private readonly UserServiceFactory _fac;

        public AllUsersAndTasksTest()
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

            var result = await _fac.UserService.AllUsersAndTasks(new BaseSearchViewModel { Keyword = "Paul" });

            //Assert

            Assert.True(result.Data.Count >= 1);
        }
    }
}
