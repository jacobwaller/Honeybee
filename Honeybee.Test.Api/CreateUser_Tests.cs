using System;
using Xunit;
using Honeybee.Test.Common.Models;
using Honeybee.Test.Common;
using FluentAssertions;

namespace Honeybee.Test.Api
{
    public class CreateUser_Tests : BaseApi_Test
    {
        public CreateUser_Tests() : base()
        {

        }

        [Fact]
        public void CreateValidUser()
        {
            var user = new CreateUserRequestModel
            {
                Name = "John Green",
                Job = "Engineer"
            };

            var details = CreateUser(user);

            var responseBody = details.DeserializeContent<CreateUserResponseModel>();
            var statusCode = (int)details.StatusCode;

            responseBody.Name.Should().Be(user.Name);
            responseBody.Job.Should().Be(user.Job);

            statusCode.Should().Be(201);
        }

        [Fact]
        public void CreateInvalidUser()
        {
            var user = new CreateUserRequestModel
            {
                Job = "Engineer"
            };

            var details = CreateUser(user);

            var responseBody = details.DeserializeContent<CreateUserResponseModel>();
            int statusCode = (int)details.StatusCode;

            responseBody.Name.Should().Be(user.Name);
            responseBody.Job.Should().Be(user.Job);

            statusCode.Should().Be(400);
        }
    }
}
