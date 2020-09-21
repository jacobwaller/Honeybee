using System;
namespace Honeybee.Test.Common.Models
{
    public class GetUserResponseModel
    {
        public DataModel Data { get; set; }
        public AdModel Ad { get; set; }
    }

    public class DataModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        // Ideally, when writing an API in .NET, you would 
        // not use snake case. However, regres' sample API does.
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Avatar { get; set; }
    }

    public class AdModel
    {
        public string Company { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
    }
}
