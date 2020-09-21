using System;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Xunit;
using Honeybee.Test.Common.Models;

namespace Honeybee.Test.Common
{
    //Causes tests to run serially
    [Collection("Sequential")]
    public class BaseApi_Test : IDisposable
    {

        protected readonly IConfigurationRoot Config;

        protected RestClient Client;

        //To handle multiple paths which may not be the same
        private readonly string CreatePath = "api/users";
        private readonly string GetPath = "api/users";

        private readonly string QA_ENDPOINT = "https://reqres.in/";

        //Test setup
        public BaseApi_Test()
        {
            Client = new RestClient(QA_ENDPOINT);
            
            Config = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.local.json", true, true)
              .AddEnvironmentVariables()
              .Build();
        }

        //Test Teardown
        public void Dispose()
        {
            CleanDatabase();
        }

        /**
         * Resets the database back to a "base state". Typically empty, or with 
         * a set of known values
         */
        public void CleanDatabase()
        {
            //TODO: Left as an exercise to the reader 
        }

        /**
         * Fetches the AuthToken from either appsettings.local.json or from the
         * environment variables if deployed
         */
        public string GetAuthToken()
        {
            return Config.GetValue<string>("QA:AuthToken");
        }

        /**
         * POSTs to our API with the CreateUserRequestModel and returns
         * an IRestResponse
         */
        public IRestResponse CreateUser(CreateUserRequestModel model)
        {
            RestRequest request = new RestRequest(CreatePath);

            //Any authentication can be done here, in one place
            var authToken = GetAuthToken();
            request.AddHeader("Authorization", authToken);
            request.AddJsonBody(model);

            return Client.Post(request);
        }

        /**
         * GETs the user with the given id
         * an IRestResponse
         */
        public IRestResponse GetUser(int id)
        {
            RestRequest request = new RestRequest(GetPath + "/" + id);

            var authToken = GetAuthToken();
            request.AddHeader("Authorization", authToken);

            return Client.Get(request);
        }
    }
}
