using System;
using Xunit;
using Honeybee.Test.Common.Models;
using Honeybee.Test.Common;
using FluentAssertions;

namespace Honeybee.Test.Api
{
    public class GetUser_Tests : BaseApi_Test
    {
        public GetUser_Tests() : base()
        {
        }

        [Fact]
        public void GetValidUser()
        {
            var user = new CreateUserRequestModel
            {
                Name = "John Green",
                Job = "Engineer"
            };

            var created = CreateUser(user).DeserializeContent<CreateUserResponseModel>();
            var id = created.Id;

            var response = GetUser(id);
            var responseBody = response.DeserializeContent<GetUserResponseModel>();
            var statusCode = (int)response.StatusCode;

            responseBody.Data.First_Name.Should().Be(user.Name.Split(" ")[0]);
            responseBody.Data.Last_Name.Should().Be(user.Name.Split(" ")[1]);

            statusCode.Should().Be(200);
        }

        [Fact]
        public void GetInvalidUser()
        {
            var id = -153726;

            var response = GetUser(id);
            var statusCode = (int)response.StatusCode;

            statusCode.Should().Be(404);
        }
    }
}
